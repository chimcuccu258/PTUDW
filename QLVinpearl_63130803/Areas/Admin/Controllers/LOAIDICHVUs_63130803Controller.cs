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
    public class LOAIDICHVUs_63130803Controller : Controller
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
        // GET: Admin/LOAIDICHVUs_63130803
        public ActionResult Index()
        {
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            return View(db.LOAIDVs.ToList());
        }

        // GET: Admin/LOAIDICHVUs_63130803/Details/5
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
            LOAIDV lOAIDV = db.LOAIDVs.Find(id);
            if (lOAIDV == null)
            {
                return HttpNotFound();
            }
            return View(lOAIDV);
        }

        // GET: Admin/LOAIDICHVUs_63130803/Create
        public ActionResult Create()
        {
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            return View();
        }

        // POST: Admin/LOAIDICHVUs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maLoaiDV,tenLoai")] LOAIDV lOAIDV)
        {
            if (ModelState.IsValid)
            {
                db.LOAIDVs.Add(lOAIDV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIDV);
        }

        // GET: Admin/LOAIDICHVUs_63130803/Edit/5
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
            LOAIDV lOAIDV = db.LOAIDVs.Find(id);
            if (lOAIDV == null)
            {
                return HttpNotFound();
            }
            return View(lOAIDV);
        }

        // POST: Admin/LOAIDICHVUs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maLoaiDV,tenLoai")] LOAIDV lOAIDV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIDV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIDV);
        }

        // GET: Admin/LOAIDICHVUs_63130803/Delete/5
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
            LOAIDV lOAIDV = db.LOAIDVs.Find(id);
            if (lOAIDV == null)
            {
                return HttpNotFound();
            }
            return View(lOAIDV);
        }

        // POST: Admin/LOAIDICHVUs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LOAIDV lOAIDV = db.LOAIDVs.Find(id);
            db.LOAIDVs.Remove(lOAIDV);
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
