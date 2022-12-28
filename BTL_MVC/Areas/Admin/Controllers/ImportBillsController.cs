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
    public class ImportBillsController : Controller
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/ImportBills
        public ActionResult Index()
        {
            var importBill = db.ImportBill.Include(i => i.Supplier).Include(i => i.Users);
            return View(importBill.ToList());
        }

        public ActionResult ViewData()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            return Json(db.ImportBill.Select(x => new { x.ImportBillId, x.ImportDate, x.Supplier.SupplierName, x.Supplierid, x.TotalPrice, x.UserId, x.Users.FullName }).ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/ImportBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBill importBill = db.ImportBill.Find(id);
            if (importBill == null)
            {
                return HttpNotFound();
            }
            return View(importBill);
        }

        // GET: Admin/ImportBills/Create
        public ActionResult Create()
        {
            ViewBag.Supplierid = new SelectList(db.Supplier, "Supplierid", "SupplierName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Admin/ImportBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportBillId,Supplierid,UserId,ImportDate,TotalPrice")] ImportBill importBill)
        {
            if (ModelState.IsValid)
            {
                db.ImportBill.Add(importBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Supplierid = new SelectList(db.Supplier, "Supplierid", "SupplierName", importBill.Supplierid);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", importBill.UserId);
            return View(importBill);
        }

        // GET: Admin/ImportBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBill importBill = db.ImportBill.Find(id);
            if (importBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Supplierid = new SelectList(db.Supplier, "Supplierid", "SupplierName", importBill.Supplierid);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", importBill.UserId);
            return View(importBill);
        }

        // POST: Admin/ImportBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportBillId,Supplierid,UserId,ImportDate,TotalPrice")] ImportBill importBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Supplierid = new SelectList(db.Supplier, "Supplierid", "SupplierName", importBill.Supplierid);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", importBill.UserId);
            return View(importBill);
        }

        // GET: Admin/ImportBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBill importBill = db.ImportBill.Find(id);
            if (importBill == null)
            {
                return HttpNotFound();
            }
            return View(importBill);
        }

        // POST: Admin/ImportBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportBill importBill = db.ImportBill.Find(id);
            db.ImportBill.Remove(importBill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "ImportBillId,Supplierid,UserId,ImportDate,TotalPrice")] ImportBill importBill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ImportBill.Add(importBill);
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
            var x = (from sp in db.ImportBill where sp.ImportBillId == id select new { sp.ImportBillId, sp.ImportDate, sp.Supplier.SupplierName, sp.Supplierid, sp.TotalPrice, sp.UserId, sp.Users.FullName }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string DeleteSup(ImportBill ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.ImportBill where sp.ImportBillId == ck.ImportBillId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.ImportBill.Remove(ds);
                    db.SaveChanges();
                    return "Xóa thông tin hóa đơn nhập thành công";
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
        public ActionResult EditData([Bind(Include = "ImportBillId,Supplierid,UserId,ImportDate,TotalPrice")] ImportBill importBill)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.ImportBill where ds.ImportBillId == importBill.ImportBillId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.ImportDate = importBill.ImportDate;
                    sp.Supplierid = importBill.Supplierid;
                    sp.UserId = importBill.UserId;
                    sp.TotalPrice = importBill.TotalPrice;
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
