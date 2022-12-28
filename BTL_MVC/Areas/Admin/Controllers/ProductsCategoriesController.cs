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
    public class ProductsCategoriesController : Controller
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/ProductsCategories
        public ActionResult Index()
        {
            return View(db.ProductsCategory.ToList());
        }
        public ActionResult ViewData()
        {
            return View();
        }
        public ActionResult GetNameCate()
        {
            return View();
        }
        public ActionResult GetAllData()
        {
            return Json(db.ProductsCategory.Select(s => new { s.CategoryId, s.CategoryName, s.Description }).ToList(), JsonRequestBehavior.AllowGet);
        }
              
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "CategoryId,CategoryName,Description")] ProductsCategory productsCategory)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.ProductsCategory.Add(productsCategory);
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
            var x = (from sp in db.ProductsCategory where sp.CategoryId == id select new { sp.CategoryId,sp.CategoryName,sp.Description}).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string DeleteCate(ProductsCategory cate)
        {
            if (cate != null)
            {
                var ds = (from sp in db.ProductsCategory where sp.CategoryId == cate.CategoryId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.ProductsCategory.Remove(ds);
                    db.SaveChanges();
                    return "Xóa sản phẩm thành công";
                }
                else
                {
                    return "Xóa sản phẩm không thành công";
                }
            }
            else
            {
                return "Xóa sản phẩm không thành công";
            }

        }

        public ActionResult EditData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "CategoryId,CategoryName,Description")] ProductsCategory productsCategory)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.ProductsCategory where ds.CategoryId == productsCategory.CategoryId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.CategoryName = productsCategory.CategoryName;
                    sp.Description = productsCategory.Description;                  
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
