using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVinpearl_63130803.Models;

namespace QLVinpearl_63130803.Areas.Admin.Controllers
{
    public class KHACHHANGs_63130803Controller : Controller
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
        // GET: Admin/KHACHHANGs_63130803
        public ActionResult Index()
        {
            //return View(db.KHACHHANGs.ToList());
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            return View(db.KHACHHANGs.ToList());
        }

        // GET: Admin/KHACHHANGs_63130803/Details/5
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
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs_63130803/Create
        string LayMaKH()
        {
            // query lấy mã cuối cùng trong bảng và parse sang số
            var maMax = db.KHACHHANGs.ToList().Select(n => n.maKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            // mã kh: KH000000 => 6 số
            string KH = maKH.ToString().PadLeft(6, '0');
            return "KH" + KH;
        }
        public ActionResult Create()
        {
            //return View();
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            ViewBag.MaKH = LayMaKH();
            return View();
        }

        // POST: Admin/KHACHHANGs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maKH,hoTenKH,SDT,diaChi,ngaySinh,gioiTinh,email,matKhau,anh,ResetPasswordCode,ResetPasswordCodeExpiration")] KHACHHANG kHACHHANG)
        {
            var imgUser = Request.Files["Avatar"];
            string postedFileName = System.IO.Path.GetFileName(imgUser.FileName);
            var path = Server.MapPath("/Content/img/Avatar" + postedFileName);
            imgUser.SaveAs(path);
            if (ModelState.IsValid)
            {
                kHACHHANG.maKH = LayMaKH();
                kHACHHANG.anh = postedFileName;
                kHACHHANG.ResetPasswordCode = null;
                kHACHHANG.ResetPasswordCodeExpiration = null;
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs_63130803/Edit/5
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
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maKH,hoTenKH,SDT,diaChi,ngaySinh,gioiTinh,email,matKhau,anh,ResetPasswordCode,ResetPasswordCodeExpiration")] KHACHHANG kHACHHANG)
        {
            var imgUser = Request.Files["Avatar"];
            try
            {
                string postedFileName = System.IO.Path.GetFileName(imgUser.FileName);
                var path = Server.MapPath("/Content/img/Avatar" + postedFileName);
                imgUser.SaveAs(path);
            }
            catch { }
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs_63130803/Delete/5
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
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
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
