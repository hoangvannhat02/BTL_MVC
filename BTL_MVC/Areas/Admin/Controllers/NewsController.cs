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
    public class NewsController : Controller
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();
        public ActionResult ViewData()
        {
            return View();
        }
        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Users);
            return View(news.ToList());
        }
        public JsonResult GetAllData()
        {
            var ds = (from sp in db.News select new { sp.NewsId, sp.Status, sp.Title, sp.UserId, sp.Users.FullName,sp.Image, sp.Content, sp.DateSubmitted }).ToList();
            return Json(ds, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult Status(News id)
        {
            var news = (from sp in db.News where sp.NewsId == id.NewsId select sp).FirstOrDefault();
            news.Status = (news.Status == true) ? false : true;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
          
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "NewsId,Title,Content,DateSubmitted,UserId,Status,Image")] News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.News.Add(news);
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
            var x = (from sp in db.News where sp.NewsId == id select new { sp.NewsId, sp.Status, sp.Title,sp.UserId,sp.Users.FullName,sp.Content,sp.DateSubmitted,sp.Image }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string DeleteNew(News ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.News where sp.NewsId == ck.NewsId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.News.Remove(ds);
                    db.SaveChanges();
                    return "Xóa bản tin thành công";
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
        public ActionResult EditData([Bind(Include = "NewsId,Title,Content,DateSubmitted,UserId,Status,Image")] News news)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.News where ds.NewsId == news.NewsId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.Title = news.Title;
                    sp.Content = news.Content;
                    sp.DateSubmitted = news.DateSubmitted;
                    sp.Status = news.Status;
                    sp.UserId = news.UserId;
                    sp.Users = news.Users;
                    sp.Image = news.Image;
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
