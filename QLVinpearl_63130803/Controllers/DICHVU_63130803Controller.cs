using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVinpearl_63130803.Models;

namespace QLVinpearl_63130803.Controllers
{
    public class DICHVU_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
        public ActionResult TimKiem()
        {
            var DICHVU = db.DICHVUs.Include(n => n.LOAIDV);
            return View(DICHVU.ToList());
        }
        [HttpPost]
        public ActionResult TimKiem(string timKiem)
        {

            var DICHVU = db.DICHVUs.Include(c => c.LOAIDV).Where(a => a.tenDV.Contains(timKiem) || a.LOAIDV.tenLoai.Contains(timKiem));
            if (DICHVU.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(DICHVU.ToList());
        }
        // GET: DICHVU_63130803
        public ActionResult Index(int? page)
        {
            int currentPage = int.Parse(Request.QueryString["page"] ?? "1");
            Session["CurrentPage"] = currentPage;
            var dICHVUs = db.DICHVUs.Include(d => d.LOAIDV);
            return View(dICHVUs.ToList());
        }

        // GET: DICHVU_63130803/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DICHVU dICHVU = db.DICHVUs.Find(id);
            if (dICHVU == null)
            {
                return HttpNotFound();
            }
            // Get the price information from VE table
            var ve = db.VEs.Where(v => v.maDV == id).FirstOrDefault();
            ViewBag.giaTien = ve?.giaTien ?? 0; // Set default value to 0 if ve is nul
            return View(dICHVU);
        }

        // GET: DICHVU_63130803/Create
        public ActionResult Create()
        {
            ViewBag.maLoaiDV = new SelectList(db.LOAIDVs, "maLoaiDV", "tenLoai");
            return View();
        }

        // POST: DICHVU_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDV,tenDV,moTa,anh,maLoaiDV,xepLoai,sdtDV,diaChiDV")] DICHVU dICHVU)
        {
            if (ModelState.IsValid)
            {
                db.DICHVUs.Add(dICHVU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLoaiDV = new SelectList(db.LOAIDVs, "maLoaiDV", "tenLoai", dICHVU.maLoaiDV);
            return View(dICHVU);
        }

        // GET: DICHVU_63130803/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DICHVU dICHVU = db.DICHVUs.Find(id);
            if (dICHVU == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLoaiDV = new SelectList(db.LOAIDVs, "maLoaiDV", "tenLoai", dICHVU.maLoaiDV);
            return View(dICHVU);
        }

        // POST: DICHVU_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDV,tenDV,moTa,anh,maLoaiDV,xepLoai,sdtDV,diaChiDV")] DICHVU dICHVU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dICHVU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLoaiDV = new SelectList(db.LOAIDVs, "maLoaiDV", "tenLoai", dICHVU.maLoaiDV);
            return View(dICHVU);
        }

        // GET: DICHVU_63130803/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DICHVU dICHVU = db.DICHVUs.Find(id);
            if (dICHVU == null)
            {
                return HttpNotFound();
            }
            return View(dICHVU);
        }

        // POST: DICHVU_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DICHVU dICHVU = db.DICHVUs.Find(id);
            db.DICHVUs.Remove(dICHVU);
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
