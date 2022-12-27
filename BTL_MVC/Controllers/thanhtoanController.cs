using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class thanhtoanController : Controller
    {
        QuanLyCuaHang db = new QuanLyCuaHang();
        // GET: lienhe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DatMua(FormCollection frm)
        {
            string mes = "";
            List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
            int id = int.Parse(Session["CustomerId"].ToString());
            Users us = db.Users.Find(id);
            //Lưu thông tin hóa đơn
            Orders od = new Orders();
            od.UserId = id;
            od.Status = false;
            od.Description = frm["Description"];
            od.DateCreated = DateTime.Today;
            od.ReceivingAddress = frm["ReceivingAddress"];
            od.TotalPrice = Convert.ToDecimal(summoney(ds));
            if (saveOrder(od) == 1)
            {
                foreach(OrderDetail item in ds)
                {
             
                    OrderDetail odt = new OrderDetail();
           

                    odt.OrderId = od.OrderId;
                    odt.ProductId = item.ProductId;
                    odt.Price = item.Price;
                    odt.Quantily = item.Quantily;
                    odt.Acount = item.Acount;
                    //savePro(pr);
                    saveOrderDetail(odt);
                }
                mes = "Thanh toán thành công";
            }
            TempData["err"] = mes;
            Session["cart"] = "";
            return RedirectToAction("thanhcong","thanhtoan");
        }
        public double summoney(List<OrderDetail> ds)
        {
            double sum = 0;
            foreach(OrderDetail item in ds)
            {
                sum += Convert.ToDouble(item.Acount);
            }
            return sum;
        }
        public void savePro(Products pr)
        {
            db.Products.Add(pr);
            db.SaveChanges();
        }
        public int saveOrder(Orders od)
        {
            db.Orders.Add(od);
            return db.SaveChanges();
        }
        public int saveOrderDetail(OrderDetail od)
        {
            db.OrderDetail.Add(od);
            return db.SaveChanges();
        }
        public ActionResult thanhcong()
        {
            //List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
            //ds.Clear();
            return View();
        }
        public JsonResult GetDataOrder()
        {
            
            return Json(db.Orders.Where(s => s.Status == false).Select(x => new { x.DateCreated,x.OrderDetail.Count, x.Users.FullName, x.TotalPrice, x.Description, x.OrderId, x.ReceivingAddress, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChiTietDonHangView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChiTietDonHang(int id)
        {
            var x = (from sp in db.OrderDetail where sp.OrderId == id select new { sp.ProductId,sp.Products.Images, sp.Products.Price,sp.Products.ProductName, sp.Quantily,sp.OrderId,sp.Orders.DateCreated,sp.Orders.Status, sp.Orders.TotalPrice, sp.Acount });
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        public JsonResult dathanhtoan()
        {
            return Json(db.Orders.Where(s => s.Status == true).Select(x => new { x.DateCreated, x.Users.FullName, x.TotalPrice, x.Description, x.OrderId, x.ReceivingAddress, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string Delete(Orders ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Orders where sp.OrderId == ck.OrderId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Orders.Remove(ds);
                    db.SaveChanges();
                    return "Hủy hóa đơn thành công";
                }
                else
                {
                    return "Hủy không thành công";
                }
            }
            else
            {
                return "Hủy không thành công";
            }

        }
    }
}