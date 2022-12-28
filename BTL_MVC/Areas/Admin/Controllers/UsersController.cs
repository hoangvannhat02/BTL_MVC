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
    public class UsersController : CheckLoginController
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult ViewData()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            var ds = (from sp in db.Users select new { sp.UserId, sp.UserName,sp.PassWord, sp.Active, sp.Address, sp.DateCreated,sp.FullName, sp.Email, sp.Role,sp.Sdt }).ToList();
            return Json(ds, JsonRequestBehavior.AllowGet);
        }      
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "UserId,UserName,PassWord,Role,Active,DateCreated,FullName,Sdt,Email,Address")] Users users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(users);
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
            var x = (from sp in db.Users where sp.UserId == id select new { sp.UserId, sp.PassWord,sp.UserName, sp.Active, sp.Address, sp.DateCreated, sp.FullName, sp.Email, sp.Role, sp.Sdt }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string DeleteNew(Users ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Users where sp.UserId == ck.UserId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Users.Remove(ds);
                    db.SaveChanges();
                    return "Xóa thông tin người dùng thành công";
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
        public ActionResult EditData([Bind(Include = "UserId,UserName,PassWord,Role,Active,DateCreated,FullName,Sdt,Email,Address")] Users users)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Users where ds.UserId == users.UserId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.UserName = users.UserName;
                    sp.PassWord = users.PassWord;
                    sp.Role = users.Role;
                    sp.Sdt = users.Sdt;
                    sp.FullName = users.FullName;
                    sp.Email = users.Email;
                    sp.DateCreated = users.DateCreated;
                    sp.Address = users.Address;
                    sp.Active = users.Active;
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
