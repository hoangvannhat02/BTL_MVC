using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class gioithieuController : Controller
    {
        QuanLyCuaHang dp = new QuanLyCuaHang();
        public PartialViewResult ViewMenu()
        {
            var ds = dp.ProductsCategory.ToList();
            return PartialView(ds);
        }
        // GET: gioithieu
        public ActionResult Index()
        {
            var ds = dp.News.Where(x => x.Status == true).FirstOrDefault();
            return View(ds);
        }
        
        public ActionResult GetData()
        {
            var ds = dp.News.Where(y => y.Status==true).Select(x => new { x.Title,x.Content,x.Users.FullName,x.DateSubmitted}).FirstOrDefault();
            //var dl = (from ds in dp.News where ds.Status == true select new { ds.NewsId, ds.Title, ds.Content, ds.DateSubmitted }).FirstOrDefault();
            return Json(ds, JsonRequestBehavior.AllowGet);
        }
    }
}