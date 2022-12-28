using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Areas.Admin.Controllers
{
    public class SuppliersController : CheckLoginController
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/Suppliers
        public ActionResult Index()
        {
            return View(db.Supplier.ToList());
        }
        public ActionResult ViewData()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            return Json(db.Supplier.Select(x => new { x.Address, x.Email, x.Sdt, x.Supplierid, x.SupplierName }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "Supplierid,SupplierName,Address,Sdt,Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Supplier.Add(supplier);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DetailDataView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailData(int id)
        {
            var x = (from sp in db.Supplier where sp.Supplierid == id select new { sp.Address, sp.Email, sp.Sdt, sp.Supplierid, sp.SupplierName }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string DeleteSup(Supplier ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Supplier where sp.Supplierid == ck.Supplierid select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Supplier.Remove(ds);
                    db.SaveChanges();
                    return "Xóa thông tin nhà cung cấp thành công";
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

        public ActionResult EditData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "Supplierid,SupplierName,Address,Sdt,Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Supplier where ds.Supplierid == supplier.Supplierid select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.SupplierName = supplier.SupplierName;
                    sp.Email = supplier.Email;
                    sp.Address = supplier.Address;
                    sp.Sdt = supplier.Sdt;
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
