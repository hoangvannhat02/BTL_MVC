using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        QuanLyCuaHang db = new QuanLyCuaHang();
        // GET: Admin/Login
        /// <summary>
        /// sadâdaad
        /// </summary>
        /// <returns></returns>
        public ActionResult login()
        {
            //if (!Session["UserAdmin"].Equals(""))
            //{
            //    return RedirectToAction("Index", "Admin");
            //}
            ViewBag.Err = "";
            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection x)
        {

            string err = "";
            string tk = x["user"];
            string mk = x["password"];
            Users ck = db.Users.Where(y => (y.UserName == tk) && y.Role == true && y.Active == true).FirstOrDefault();
            if(ck == null)
            {
                err = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (ck.PassWord.Equals(mk))
                {
                    Session["UserAdmin"] = ck.UserName;
                    Session["UserId"] = ck.PassWord;
                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    err = "Mật khẩu không chính xác";
                }
            }
            ViewBag.Err = err;
            return View();

        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
         
            return RedirectToAction("login", "Login");
        }
    }
}