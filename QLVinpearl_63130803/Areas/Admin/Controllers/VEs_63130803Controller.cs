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
    public class VEs_63130803Controller : Controller
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
        // GET: Admin/VEs_63130803
        public ActionResult Index()
        {
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            var vEs = db.VEs.Include(v => v.DICHVU);
            return View(vEs.ToList());
        }
        // GET: Admin/VEs_63130803/Details/5
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
            VE vE = db.VEs.Find(id);
            if (vE == null)
            {
                return HttpNotFound();
            }
            return View(vE);
        }
        string LayMaVe()
        {
            var maMax = db.VEs.ToList().Select(n => n.maVe).Max();
            int maVe = int.Parse(maMax.Substring(2)) + 1;
            string Ve = maVe.ToString().PadLeft(6, '0');
            return "VE" + Ve;
        }
        // GET: Admin/VEs_63130803/Create
        public ActionResult Create()
        {
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            ViewBag.maDV = new SelectList(db.DICHVUs, "maDV", "tenDV");
            return View();
        }

        // POST: Admin/VEs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maVe,maDV,loaiVe,giaTien")] VE vE)
        {
            if (ModelState.IsValid)
            {
                db.VEs.Add(vE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maDV = new SelectList(db.DICHVUs, "maDV", "tenDV", vE.maDV);
            return View(vE);
        }

        // GET: Admin/VEs_63130803/Edit/5
        public ActionResult Edit(string id)
        {
            if (CheckPermission("CN03") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VE vE = db.VEs.Find(id);
            if (vE == null)
            {
                return HttpNotFound();
            }
            ViewBag.maDV = new SelectList(db.DICHVUs, "maDV", "tenDV", vE.maDV);
            return View(vE);
        }

        // POST: Admin/VEs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maVe,maDV,loaiVe,giaTien")] VE vE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maDV = new SelectList(db.DICHVUs, "maDV", "tenDV", vE.maDV);
            return View(vE);
        }

        // GET: Admin/VEs_63130803/Delete/5
        public ActionResult Delete(string id)
        {
            if (CheckPermission("CN04") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VE vE = db.VEs.Find(id);
            if (vE == null)
            {
                return HttpNotFound();
            }
            return View(vE);
        }

        // POST: Admin/VEs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VE vE = db.VEs.Find(id);
            db.VEs.Remove(vE);
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
