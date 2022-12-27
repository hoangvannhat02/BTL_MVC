using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MVC.Models;

namespace BTL_MVC.Controllers
{
    public class HomeController : Controller
    {
        QuanLyCuaHang dp = new QuanLyCuaHang();
        public PartialViewResult ViewMenu()
        {
            var ds = dp.ProductsCategory.ToList();
            return PartialView(ds);
        }
        public ActionResult getCategory()
        {
            return Json(dp.ProductsCategory.Select(x => new { x.CategoryId, x.CategoryName, x.Description }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getcateid(int id)
        {
            var query = (from sp in dp.Products where sp.CategoryId == id select new { sp.ProductId, sp.ProductName, sp.Images ,sp.Price }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getallcate()
        {
            var query = (from sp in dp.Products select new { sp.ProductId, sp.ProductName,
                        sp.Images, sp.Price }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getcatewrite()
        {
            string x = "Đồ uống";
            return Json(dp.Products.Where(s => s.ProductsCategory.CategoryName == x).Select(y => new { y.ProductName, y.Price, y.ProductId, y.Images }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult share(string x)
        {
            var query = (from sp in dp.Products where sp.ProductName.Contains(x) select new {sp.ProductName});
            return Json(query,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(int id)
        {
            var ds = dp.Products.Where(x => x.CategoryId == id).ToList();
            return View(ds);
        }
        public ActionResult DetailPro(int id)
        {
            var ds = dp.Products.FirstOrDefault(x => x.ProductId == id);
            return View(ds);
        }
        public ActionResult DetailPro2(int id)
        {
            var ds = dp.Products.FirstOrDefault(x => x.ProductId == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult AddCart(Products id)
        {
            int sumsp = 0;
            var sp = dp.Products.FirstOrDefault(s => s.ProductId == id.ProductId);
            if (Session["cart"].Equals(""))
            {
                List<OrderDetail> ds = new List<OrderDetail>();
                OrderDetail dt = new OrderDetail() { ProductId = id.ProductId, Quantily = id.Quantily, Acount = 1, Products = sp };
                dt.Acount = dt.Quantily * dt.Products.Price;
                ds.Add(dt);
                Session["cart"] = ds;
            }
            else
            {
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                var kt = ds.FirstOrDefault(s => s.ProductId == id.ProductId);

                if (kt == null)
                {
                    OrderDetail dt = new OrderDetail() { ProductId = id.ProductId, Quantily = id.Quantily, Acount = 1, Products = sp };
                    dt.Acount = dt.Quantily * dt.Products.Price;
                    ds.Add(dt);
                    Session["cart"] = ds;
                }
                else
                {
                    kt.Quantily += id.Quantily;
                }
                Session["cart"] = ds;
            }
            List<OrderDetail> dssp = (List<OrderDetail>)Session["cart"];
            foreach(var x in dssp)
            {
                sumsp += Convert.ToInt32(x.Quantily);
            }
            return Json(new { Acount = sumsp },JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCart(int id)
        {
          
            var sp = dp.Products.FirstOrDefault(s => s.ProductId == id);
            if (Session["cart"].Equals(""))
            {
                List<OrderDetail> ds = new List<OrderDetail>();
                OrderDetail dt = new OrderDetail() { ProductId = id, Quantily = 1, Acount = 1, Products = sp };
                dt.Acount = dt.Quantily * dt.Products.Price;
                ds.Add(dt);
                Session["cart"] = ds;
            }
            else
            {
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                var kt = ds.FirstOrDefault(s => s.ProductId == id);

                if (kt == null)
                {
                    OrderDetail dt = new OrderDetail() { ProductId = id, Quantily =1, Acount = 1, Products = sp };
                    dt.Acount = dt.Quantily * dt.Products.Price;
                    ds.Add(dt);
                    Session["cart"] = ds;
                }
                else
                {
                    kt.Quantily += 1;
                }
                Session["cart"] = ds;
            }
            
            return RedirectToAction("ViewCart");
        }
        public void cartdelete(int productid)
        {
            if (!System.Web.HttpContext.Current.Session["cart"].Equals(""))
            {
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                int vt = 0;
                foreach (var item in ds)
                {
                    if (item.ProductId == productid)
                    {
                        ds.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                Session["cart"] = ds;
            }
        }
        public ActionResult ViewCart()
        {
            if (!Session["cart"].Equals(""))
            {
                List<OrderDetail> dsdh = (List<OrderDetail>)Session["cart"];
                List<OrderDetail> ds = new List<OrderDetail>();
                foreach (var item in dsdh)
                {
                    ds.Add(item);
                }
                return View(ds);
            }

            return View();
        }
        public ActionResult GetDataCart()
        {
            List<OrderDetail> dsdh = (List<OrderDetail>)Session["cart"];
            List<OrderDetail> ds = new List<OrderDetail>();
            foreach(var item in dsdh)
            {
                ds.Add(item);
            }
            return Json(ds,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var ds = dp.ProductsCategory.ToList();
            return View(ds);
        }
       
        public ActionResult TimKiem(FormCollection frm)
        {
            string tk = frm["search"];
            if (tk == null)
            {
                var x = dp.Products.ToList();
                return View("TimKiem", x);
            }

            else
            {
                List<Products> ds = dp.Products.Where(x => x.ProductName.Contains(tk) || 
                                    x.Price.ToString().Contains(tk) || x.Description.Contains(tk)).ToList();
                return View(ds);
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult GetListCategory(int id)
        {
            var kq = dp.Products.SqlQuery($"Select * from Products where ProductId = {id}");
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}