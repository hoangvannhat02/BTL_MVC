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
    public class ImportBillDetailsController : Controller
    {
        private QuanLyCuaHang db = new QuanLyCuaHang();

        // GET: Admin/ImportBillDetails
        public ActionResult Index()
        {
            var importBillDetail = db.ImportBillDetail.Include(i => i.ImportBill).Include(i => i.Products);
            return View(importBillDetail.ToList());
        }

        // GET: Admin/ImportBillDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBillDetail importBillDetail = db.ImportBillDetail.Find(id);
            if (importBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(importBillDetail);
        }

        // GET: Admin/ImportBillDetails/Create
        public ActionResult Create()
        {
            ViewBag.ImportBillId = new SelectList(db.ImportBill, "ImportBillId", "ImportBillId");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/ImportBillDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportBillId,ProductId,Quantily,Price")] ImportBillDetail importBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.ImportBillDetail.Add(importBillDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImportBillId = new SelectList(db.ImportBill, "ImportBillId", "ImportBillId", importBillDetail.ImportBillId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", importBillDetail.ProductId);
            return View(importBillDetail);
        }

        // GET: Admin/ImportBillDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBillDetail importBillDetail = db.ImportBillDetail.Find(id);
            if (importBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImportBillId = new SelectList(db.ImportBill, "ImportBillId", "ImportBillId", importBillDetail.ImportBillId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", importBillDetail.ProductId);
            return View(importBillDetail);
        }

        // POST: Admin/ImportBillDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportBillId,ProductId,Quantily,Price")] ImportBillDetail importBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importBillDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImportBillId = new SelectList(db.ImportBill, "ImportBillId", "ImportBillId", importBillDetail.ImportBillId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", importBillDetail.ProductId);
            return View(importBillDetail);
        }

        // GET: Admin/ImportBillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportBillDetail importBillDetail = db.ImportBillDetail.Find(id);
            if (importBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(importBillDetail);
        }

        // POST: Admin/ImportBillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportBillDetail importBillDetail = db.ImportBillDetail.Find(id);
            db.ImportBillDetail.Remove(importBillDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
