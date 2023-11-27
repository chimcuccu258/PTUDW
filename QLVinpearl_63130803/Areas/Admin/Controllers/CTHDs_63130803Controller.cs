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
    public class CTHDs_63130803Controller : Controller
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
        // GET: Admin/CTHDs_63130803
        public ActionResult Index()
        {
            //var cTHDs = db.CTHDs.Include(c => c.HOADON).Include(c => c.VE);
            //return View(cTHDs.ToList());
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            var cTHD = db.CTHDs.Include(c => c.VE);
            return View(cTHD.ToList());
        }

        // GET: Admin/CTHDs_63130803/Details/5
        public ActionResult Details(string maHD, string maVe)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CTHD cTHD = db.CTHDs.Find(id);
            //if (cTHD == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cTHD);
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (maHD == null || maVe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHD cTHD = db.CTHDs.Find(maHD, maVe);
            if (cTHD == null)
            {
                return HttpNotFound();
            }

            //ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV", cTHD.maVe);
            return View(cTHD);
        }

        // GET: Admin/CTHDs_63130803/Create
        public ActionResult Create()
        {
            //ViewBag.maHD = new SelectList(db.HOADONs, "maHD", "maKH");
            //ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV");
            //return View();
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV");
            return View();
        }

        // POST: Admin/CTHDs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maHD,maVe,soLuong,giaTien")] CTHD cTHD)
        {
            if (ModelState.IsValid)
            {
                db.CTHDs.Add(cTHD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.maHD = new SelectList(db.HOADONs, "maHD", "maKH", cTHD.maHD);
            ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV", cTHD.maVe);
            return View(cTHD);
        }

        // GET: Admin/CTHDs_63130803/Edit/5
        public ActionResult Edit(string maHD, string maVe)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CTHD cTHD = db.CTHDs.Find(id);
            //if (cTHD == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.maHD = new SelectList(db.HOADONs, "maHD", "maKH", cTHD.maHD);
            //ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV", cTHD.maVe);
            //return View(cTHD);
            if (CheckPermission("CN03") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (maHD == null || maVe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CTHD cTHD = db.CTHDs.SingleOrDefault(x => x.maHD == maHD && x.maVe == maVe);

            if (cTHD == null)
            {
                return HttpNotFound();
            }

            ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV", cTHD.maVe);
            return View(cTHD);
        }

        // POST: Admin/CTHDs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maHD,maVe,soLuong,giaTien")] CTHD cTHD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTHD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.maHD = new SelectList(db.HOADONs, "maHD", "maKH", cTHD.maHD);
            ViewBag.maVe = new SelectList(db.VEs, "maVe", "maDV", cTHD.maVe);
            return View(cTHD);
        }

        // GET: Admin/CTHDs_63130803/Delete/5
        public ActionResult Delete(string maHD, string maVe)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CTHD cTHD = db.CTHDs.Find(id);
            //if (cTHD == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cTHD);
            if (CheckPermission("CN04") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (maHD == null || maVe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CTHD cTHD = db.CTHDs.SingleOrDefault(x => x.maHD == maHD && x.maVe == maVe);

            if (cTHD == null)
            {
                return HttpNotFound();
            }

            return View(cTHD);
        }

        // POST: Admin/CTHDs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string maHD, string maVe)
        {
            //CTHD cTHD = db.CTHDs.Find(id);
            //db.CTHDs.Remove(cTHD);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            CTHD cTHD = db.CTHDs.SingleOrDefault(x => x.maHD == maHD && x.maVe == maVe);

            if (cTHD == null)
            {
                return HttpNotFound();
            }

            db.CTHDs.Remove(cTHD);
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
