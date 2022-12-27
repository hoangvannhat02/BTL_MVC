using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_MVC.Areas.Admin.Controllers
{
    public class AdminController : CheckLoginController
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            //if(Session["user"] == null)
            //{
            //    return RedirectToAction("dangnhap", "Login");
            //    return Redirect("/Controllers/Login/dangnhap");
            //}
            return View();
        }
    }
}