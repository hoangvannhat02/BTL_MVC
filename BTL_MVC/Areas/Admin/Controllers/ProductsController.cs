using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Areas.Admin.Controllers
{
    public class ProductsController : CheckLoginController
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();
        private Products pr = new Products();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductsCategory);
            return View(products.ToList());
        }

        public ActionResult GetAllData()
        {
            return Json(db.Products.Select(x => new { x.ProductsCategory.CategoryName, x.Description, x.Images, x.ProductId, x.ProductName, x.Price, x.Quantily, x.Content }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewData()
        {
            return View();
        }     
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Size,Quantily,Price,Description,Content")] Products products)
        {
            products.MoreImages = "";
            products.Size = "";
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Products.Add(products);
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
            var x = (from sp in db.Products where sp.ProductId == id select new { sp.ProductId,sp.ProductName,sp.Content,sp.Quantily,sp.Size,sp.Price,sp.Images,sp.MoreImages,sp.Description,sp.ProductsCategory.CategoryName,sp.ProductsCategory.CategoryId}).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DeleteProduct(Products product)
        {
            if (product != null)
            {
                var ds = (from sp in db.Products where sp.ProductId == product.ProductId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Products.Remove(ds);
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
        public ActionResult EditData([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Size,Quantily,Price,Description,Content")] Products products)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Products where ds.ProductId == products.ProductId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.ProductName = products.ProductName;
                    sp.Description = products.Description;
                    sp.Quantily = products.Quantily;
                    sp.MoreImages = products.MoreImages;
                    sp.Size = products.Size ;
                    sp.Price = products.Price;
                    sp.Images = products.Images  ;
                    sp.Content = products.Content ;
                    sp.CategoryId = products.CategoryId ;
                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { msg = true },JsonRequestBehavior.AllowGet);
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
