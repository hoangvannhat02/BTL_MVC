using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class doanController : Controller
    {

        QuanLyCuaHang db = new QuanLyCuaHang();
        // GET: doan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult detailpro(int id)
        {
            var query = (from sp in db.Products where sp.ProductId == id select new { sp.ProductId, sp.ProductName, sp.Price, 
                sp.Content, sp.Images,sp.Quantily }).FirstOrDefault();
            return Json(query,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCategory()
        {
            var query = (from sp in db.ProductsCategory select new { sp.CategoryId,sp.CategoryName }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllSuppliers()
        {
            var query = (from sp in db.Supplier select new { sp.Supplierid, sp.SupplierName }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}