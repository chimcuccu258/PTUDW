using QLVinpearl_63130803.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLVinpearl_63130803.Controllers
{
    public class LoginKhachHang_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
        // GET: LoginKhachHang_63130803
        public ActionResult Index()
        {
            return View();
        }
        private bool IsEmailExists(string email)
        {
            return db.KHACHHANGs.Any(kh => kh.email == email);
        }
        string LayMaKH()
        {
            // query lấy mã cuối cùng trong bảng và parse sang số
            var maMax = db.KHACHHANGs.ToList().Select(n => n.maKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            // mã kh: KH000000 => 6 số
            string KH = maKH.ToString().PadLeft(6, '0');
            return "KH" + KH;
        }
        // GET: Customer/Register
        public ActionResult Register()
        {
            ViewBag.MaKH = LayMaKH();
            return View();
        }
		// POST: Customer/Register
		[HttpPost]
		public ActionResult Register(KHACHHANG model)
		{

			if (IsEmailExists(model.email))
			{
				ModelState.AddModelError("Email", "Email đã được sử dụng.");
				return View(model);
			}

			if (ModelState.IsValid)
			{
				Session["EmailKH"] = model.email;
				model.maKH = LayMaKH();
				db.KHACHHANGs.Add(model);
				db.SaveChanges();
				return RedirectToAction("Index", "DICHVU_63130803");
			}
			return View(model);
		}

		// GET: LoginKhachHang
		public ActionResult Login()
		{
			return View();
		}
		// POST: LoginKhachHang
		[HttpPost]
		public ActionResult Login(LoginKhachHangModel_63130803 model)
		{
			var user = db.KHACHHANGs.SingleOrDefault(kh => kh.email == model.Email);
			if (user == null)
			{
				ModelState.AddModelError("Email", "Tài khoản không tồn tại!");
			}
			else if (user.matKhau == model.MKhau)
			{
				Session["EmailKH"] = model.Email;
				return RedirectToAction("Index", "DICHVU_63130803");
			}
			else if (user.matKhau != model.MKhau)
			{
				ModelState.AddModelError("MKhau", "Mật khẩu không chính xác!");
			}
			else
			{
				ModelState.AddModelError("", "Thông tin tài khoản không hợp lệ!");
			}
			return View("Login");
		}

		public ActionResult Logout()
		{
			Session["EmailKH"] = null;
			return RedirectToAction("Index", "DICHVU_63130803");
		}
	}
}