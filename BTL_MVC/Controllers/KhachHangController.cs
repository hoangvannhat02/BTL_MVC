using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class KhachHangController : Controller
    {
        QuanLyCuaHang dp = new QuanLyCuaHang();
        // GET: KhachHang
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection frm)
        {
            string tk = frm["username"];
            string mk = frm["password"];
            string err = "";
            Users ck = dp.Users.Where(x => x.UserName == tk && x.Active == true && x.Role ==false).FirstOrDefault();
            if (ck == null)
            {
                err = "Tài khoản không tồn tại";
            }
            else
            {
                if (ck.PassWord.Equals(mk))
                {
                    Session["UserCustomer"] = tk;
                    Session["CustomerId"] = ck.UserId;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    err = "Mật khẩu không chính xác";
                }
            }
            ViewBag.err = err;
            return View("DangNhap");
        }
        public ActionResult ViewProduct(int id)
        {
            var ds = dp.Products.Where(x => x.CategoryId == id).ToList();
            return View("ViewProduct",ds);
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "UserId,UserName,PassWord,Role,Active,DateCreated,FullName,Sdt,Email,Address")] Users users)
        {
            string err = "";
            Users ck = dp.Users.Where(x => x.UserName == users.UserName).FirstOrDefault();
            if (ck == null)
            {
                users.Role = false;
                users.Active = true;
                users.DateCreated = DateTime.Now;
                if (ModelState.IsValid)
                {
                    dp.Users.Add(users);
                    dp.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            else
            {
                err = "Tài khoản đã tồn tại vui lòng thử tài khoản khác";
            }
            ViewBag.err = err;
            return View("DangKy");
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            return Redirect("~/KhachHang/DangNhap");
        }
    }
}