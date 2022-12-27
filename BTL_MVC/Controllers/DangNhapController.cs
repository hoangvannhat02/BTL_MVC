using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class DangNhapController : Controller
    {
        QuanLyCuaHang db = new QuanLyCuaHang();
        // GET: Login
        public ActionResult dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangnhap(string user,string password)
        {

            if(user == "admin" && password == "123")
            {
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["err"] = "Tài khoản không chính xác";
                return View();
            }
            
        }
    }
}