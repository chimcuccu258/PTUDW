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
    public class LoaiDV_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
		//Danh sách dịch vụ theo loại dịch vụ
		public ActionResult DanhSachTheoLoaiDV(string loaiDV)
		{
			var dichVu = db.DICHVUs.Where(dv => dv.LOAIDV.tenLoai == loaiDV);
			return View(dichVu.ToList());
		}

		// GET: LOAIDV/Details/5
		public ActionResult Details(string id)
		{
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

		// GET: LOAIDV/Create


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