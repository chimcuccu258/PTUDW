using QLVinpearl_63130803.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLVinpearl_63130803.Areas.Admin.Controllers
{
    public class Dashboard_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();

        // GET: Admin/Dashboard_63130803
        public ActionResult Index()
        {
			// Đếm số lượng khách hàng trong bảng "KHACHHANG"
			int countKH = db.KHACHHANGs.Count();

			// Đếm số lượng dịch vụ trong bảng "DICHVU"
			int countDV = db.DICHVUs.Count();

			// Đếm số lượng hóa đơn trong bảng "HOADON"
			int countHD = db.HOADONs.Count();

			// Đếm số lượng vé trong bảng "VE"
			int countVe = db.VEs.Count();

			// Lưu trữ các giá trị đếm vào các thuộc tính của đối tượng "ViewBag"
			ViewBag.CountKH = countKH;
			ViewBag.CountDV = countDV;
			ViewBag.CountHD = countHD;
			ViewBag.CountVe = countVe;

			return View();
        }
    }
}