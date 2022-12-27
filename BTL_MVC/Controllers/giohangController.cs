using BTL_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace BTL_MVC.Controllers
{
    public class giohangController : Controller
    {
        QuanLyCuaHang db = new QuanLyCuaHang();
        // GET: tintuc
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult GetAllCart() {
        //    List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
        //    return Json(ds, JsonRequestBehavior.AllowGet);
        //  }
        public ActionResult cartdelete(int productid)
        {
            if (!Session["cart"].Equals(""))
            {
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                var check = ds.SingleOrDefault(x => x.ProductId == productid);
                if(check != null)
                {
                    ds.Remove(check);
                }
                //int vt = 0;
                //foreach (var item in ds)
                //{
                //    if (item.ProductId == productid)
                //    {
                //        ds.RemoveAt(vt);
                //        break;
                //    }
                //    vt++;
                //}
                Session["cart"] = ds;
            }
            return RedirectToAction("ViewCart","Home");
        }
        [HttpPost]
        public ActionResult cartupdate(FormCollection x)
        {
            //var listsl = Convert.ToInt32(frm["quantily"]);
            //var id = Convert.ToInt32(frm["id"]);

            //List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
            //var check = ds.SingleOrDefault(s => s.ProductId == id);
            //if(check != null)
            //{
            //    check.Quantily = listsl;
            //    check.Acount = listsl * check.Products.Price;
            //}
            //Session["cart"] = check;
            //return RedirectToAction("ViewCart", "Home");

          
                var listsl = x["quantily"]; //mảng chứa lần lượt các giá trị số lượng của giỏ hàng
                var listarr = listsl.Split(',');
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                int vt = 0;
                foreach (OrderDetail item in ds)
                {
                    ds[vt].Quantily = int.Parse(listarr[vt]);
                    ds[vt].Acount = ds[vt].Products.Price * ds[vt].Quantily;
                    vt++;
                }
                Session["cart"] = ds;

            return RedirectToAction("ViewCart", "Home");
        }
        public ActionResult DeleteAll()
        {
            //Session["cart"] = "";                   
            return RedirectToAction("ViewCart", "Home");
        }
        public ActionResult ThanhToan()
        {
            List<OrderDetail> dsdh = (List<OrderDetail>)Session["cart"];
            if (Session["UserCustomer"].Equals(""))
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            int id = int.Parse(Session["CustomerId"].ToString());
            Users us = db.Users.Find(id);
            ViewBag.customer = us;
            return View("ThanhToan",dsdh);
        }
    }
}