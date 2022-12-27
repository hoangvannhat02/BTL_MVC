using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;
using System.IO;
using System.Text;

namespace BTL_MVC.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Users);
            return View(orders.ToList());
        }
        public JsonResult GetAllData()
        {
            return Json(db.Orders.Select(x=>new { x.DateCreated,x.TotalPrice,x.Description,x.OrderId,x.ReceivingAddress,x.Status,x.UserId,x.Users.FullName}),JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetDetailData(int id)
        {
            return Json(db.Orders.Where(y => y.OrderId == id).Select(x => new { x.OrderId, x.Status, x.Users.FullName, x.ReceivingAddress,x.TotalPrice, x.Description, x.DateCreated }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBilloff()
        {
            return Json(db.Orders.Where(s=>s.Status==false).Select(x => new { x.DateCreated,x.Users.FullName, x.TotalPrice, x.Description, x.OrderId, x.ReceivingAddress, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBillon()
        {
            return Json(db.Orders.Where(s => s.Status == true).Select(x => new { x.DateCreated, x.Users.FullName, x.TotalPrice, x.Description, x.OrderId, x.ReceivingAddress, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewData()
        {
            return View();
        }
        [HttpPost]
        public string DeleteOrder(Orders ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Orders where sp.OrderId == ck.OrderId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Orders.Remove(ds);
                    db.SaveChanges();
                    return "Xóa hóa đơn thành công";
                }
                else
                {
                    return "Xóa không thành công";
                }
            }
            else
            {
                return "Xóa không thành công";
            }

        }
        [HttpPost]
        public JsonResult Status(Orders id)
        {
        
            var news = (from sp in db.Orders where sp.OrderId == id.OrderId select sp).FirstOrDefault();
            news.Status = true;
            var odid = db.OrderDetail.Where(x=>x.OrderId==news.OrderId).ToList();           
            foreach(OrderDetail item in odid)
            {
                var pro = db.Products.Find(item.ProductId);
                pro.Quantily -= item.Quantily;
                savePro(pro);
            }
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public void savePro(Products pr)
        {
            db.Entry(pr).State = EntityState.Modified;         
            db.SaveChanges();
        }
        public ActionResult ChiTietDonHangView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChiTietDonHang(int id)
        {
            var x = (from sp in db.OrderDetail where sp.OrderId == id select new { sp.ProductId, sp.Products.Images, sp.Products.Price, sp.Products.ProductName, sp.Quantily, sp.OrderId, sp.Orders.DateCreated, sp.Orders.Status, sp.Orders.TotalPrice, sp.Acount });
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ChiTietKhach(int id)
        {
            var x = (from sp in db.Orders join kh in db.Users on sp.UserId equals kh.UserId 
                     where sp.OrderId == id select new { kh.UserId,kh.FullName,kh.Sdt,kh.Email,kh.Address}).ToList();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSum(int id)
        {
            var x = (from sp in db.Orders where sp.OrderId==id select new { sp.TotalPrice}).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "OrderId,DateCreated,Status,TotalPrice,UserId,ReceivingAddress,Description")] Orders oder)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Orders where ds.OrderId == oder.OrderId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.DateCreated = oder.DateCreated;
                    sp.Status = oder.Status;
                    sp.ReceivingAddress = oder.ReceivingAddress;
                    sp.Description = oder.Description;
                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
