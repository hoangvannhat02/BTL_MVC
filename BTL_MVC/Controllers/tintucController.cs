using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class tintucController : Controller
    {
        QuanLyCuaHang dp = new QuanLyCuaHang();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            return Json(dp.News.Select(x => new { x.Content, x.DateSubmitted, x.Image, x.NewsId, x.Title, x.Users.FullName }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DataView()
        {
            return View();
        }
        public JsonResult GetDataDetail(int id)
        {
            return Json(dp.News.Where(y=>y.NewsId==id).Select(x => new { x.Content, x.DateSubmitted, x.Image, x.NewsId, x.Title, x.Users.FullName }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
    }
}