using QLVinpearl_63130803.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLVinpearl_63130803.Areas.Admin.Controllers
{
    public class Login_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();

        // GET: Admin/Login_63130803
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Login(LoginAdminModel_63130803 model)
		{
			// query vào bảng NV và check các điều kiện.
			var user = (from nv in db.NHANVIENs
						join lnv in db.LOAINVs on nv.maLoaiNV equals lnv.maLoaiNV
						where nv.email == model.Email
						select new
						{
							NhanVien = nv,
							LoaiNhanVien = lnv
						}).SingleOrDefault();

			if (user == null)
			{
				ModelState.AddModelError("", "Tài khoản không tồn tại!");
			}
			else if (user.NhanVien.matKhau == model.MKhau)
			{
				// tạo session để lưu dữ liệu trong phiên làm việc
				Session.Add("Email", model.Email);
				Session.Add("TenTaiKhoan", user.NhanVien.hoTenNV);
				Session.Add("maLNV", user.LoaiNhanVien.maLoaiNV);
				return RedirectToAction("Index", "Dashboard_63130803");
			}
			else if (user.NhanVien.matKhau != model.MKhau)
			{
				ModelState.AddModelError("", "Mật khẩu không hợp lệ!");
			}
			else
			{
				ModelState.AddModelError("", "Thông tin tài khoản không hợp lệ!");
			}
			return View("Index");
		}
		public ActionResult Logout()
		{
			// destroy session
			Session["Email"] = null;
			Session["TenTaiKhoan"] = null;
			Session["maLNV"] = null;
			return RedirectToAction("Index", "Login_63130803");
		}
	}
}