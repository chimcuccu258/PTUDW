using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVinpearl_63130803.Models;

namespace QLVinpearl_63130803.Areas.Admin.Controllers
{
    public class SOCAs_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
        // Kiểm tra quyền của nhân viên
        public bool CheckPermission(string maChucNang)
        {
            if (Session["maLNV"] == null) Response.Redirect("~/Admin/Login_63130803/Index");
            var userSession = Session["maLNV"].ToString();
            var count = db.PHANQUYENs.Count(m => m.maLoaiNV == userSession && m.maChucNang == maChucNang);
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        // GET: Admin/SOCAs_63130803
        public ActionResult Index()
        {
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            var sOCAs = db.SOCAs.Include(s => s.NHANVIEN);
            return View(sOCAs.ToList());
        }

        // GET: Admin/SOCAs_63130803/Details/5
        public ActionResult Details(string id)
        {
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOCA sOCA = db.SOCAs.Find(id);
            if (sOCA == null)
            {
                return HttpNotFound();
            }
            return View(sOCA);
        }

        // GET: Admin/SOCAs_63130803/Create
        public ActionResult Create()
        {
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV");
            return View();
        }

        // POST: Admin/SOCAs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maCa,maNV,soCa1")] SOCA sOCA)
        {
            if (ModelState.IsValid)
            {
                db.SOCAs.Add(sOCA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", sOCA.maNV);
            return View(sOCA);
        }

        // GET: Admin/SOCAs_63130803/Edit/5
        public ActionResult Edit(string maCa, string maNV)
        {
            if (CheckPermission("CN03") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (maCa == null || maNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOCA sOCA = db.SOCAs.SingleOrDefault(x => x.maCa == maCa && x.maNV == maNV);
            if (sOCA == null)
            {
                return HttpNotFound();
            }
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", sOCA.maNV);
            return View(sOCA);
        }

        // POST: Admin/SOCAs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maCa,maNV,soCa1")] SOCA sOCA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sOCA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", sOCA.maNV);
            return View(sOCA);
        }

        // GET: Admin/SOCAs_63130803/Delete/5
        public ActionResult Delete(string maCa, string maNV)
        {
            if (CheckPermission("CN04") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (maCa == null || maNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOCA sOCA = db.SOCAs.SingleOrDefault(x => x.maCa == maCa && x.maNV == maNV);
            if (sOCA == null)
            {
                return HttpNotFound();
            }
            return View(sOCA);
        }

        // POST: Admin/SOCAs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string maCa, string maNV)
        {
            SOCA sOCA = db.SOCAs.SingleOrDefault(x => x.maCa == maCa && x.maNV == maNV);
            if (sOCA == null)
            {
                return HttpNotFound();
            }
            db.SOCAs.Remove(sOCA);
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
