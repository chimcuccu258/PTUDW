using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using QLVinpearl_63130803.Models;

namespace QLVinpearl_63130803.Areas.Admin.Controllers
{
    public class HOADONs_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
        // Kiểm tra quyền của nhân viên
        public bool CheckPermission(string maChucNang)
        {
            if (Session["maLNV"] == null) Response.Redirect("~/Admin/Login/Index");
            var userSession = Session["maLNV"].ToString();
            var count = db.PHANQUYENs.Count(m => m.maLoaiNV == userSession && m.maChucNang == maChucNang);
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        // GET: Admin/HOADONs_63130803
        public ActionResult Index()
        {
            if (CheckPermission("CN01") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            var hOADONs = db.HOADONs.Include(h => h.KHACHHANG).Include(h => h.NHANVIEN);
            return View(hOADONs.ToList());
        }

        // GET: Admin/HOADONs_63130803/Details/5
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
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // GET: Admin/HOADONs_63130803/Create
        public ActionResult Create()
        {
            if (CheckPermission("CN02") == false)
            {
                Response.Redirect("~/Admin/PermissionError_63130803/NotAllowPermission");
            }
            ViewBag.maKH = new SelectList(db.KHACHHANGs, "maKH", "hoTenKH");
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV");
            ViewBag.maTrangThai = new SelectList(db.TRANGTHAIHDs, "maTrangThai", "tenTrangThai");
            return View();
        }

        // POST: Admin/HOADONs_63130803/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maHD,maKH,maNV,ngayThanhToan,SDT,email")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.HOADONs.Add(hOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKH = new SelectList(db.KHACHHANGs, "maKH", "hoTenKH", hOADON.maKH);
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", hOADON.maNV);
            ViewBag.maTrangThai = new SelectList(db.TRANGTHAIHDs, "maTrangThai", "tenTrangThai", hOADON.maTrangThai);
            return View(hOADON);
        }

        // GET: Admin/HOADONs_63130803/Edit/5
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
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKH = new SelectList(db.KHACHHANGs, "maKH", "hoTenKH", hOADON.maKH);
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", hOADON.maNV);
            ViewBag.maTrangThai = new SelectList(db.TRANGTHAIHDs, "maTrangThai", "tenTrangThai", hOADON.maTrangThai);
            return View(hOADON);
        }

        // POST: Admin/HOADONs_63130803/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maHD,maKH,maNV,ngayThanhToan,SDT,email,maTrangThai")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKH = new SelectList(db.KHACHHANGs, "maKH", "hoTenKH", hOADON.maKH);
            ViewBag.maNV = new SelectList(db.NHANVIENs, "maNV", "maLoaiNV", hOADON.maNV);
            ViewBag.maTrangThai = new SelectList(db.TRANGTHAIHDs, "maTrangThai", "tenTrangThai", hOADON.maTrangThai);
            return View(hOADON);
        }

        // GET: Admin/HOADONs_63130803/Delete/5
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
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: Admin/HOADONs_63130803/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOADON hOADON = db.HOADONs.Find(id);
            db.HOADONs.Remove(hOADON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            // query join 2 bảng để lấy dữ liệu hoá đơn
            var query = from hd in db.HOADONs
                        join cthd in db.CTHDs on hd.maHD equals cthd.maHD
                        select new
                        {
                            HOADON = hd,
                            CTHD = cthd
                        };
            var listHoaDon = query.ToList();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // tạo tên của sheet excel
                var worksheet = package.Workbook.Worksheets.Add("Hoadons");

                // add tên của các cột
                worksheet.Cells[1, 1].Value = "Mã HD";
                worksheet.Cells[1, 2].Value = "Mã KH";
                worksheet.Cells[1, 3].Value = "Mã NV";
                worksheet.Cells[1, 3].Value = "Trạng thái";
                worksheet.Cells[1, 4].Value = "Ngày Thanh Toán";
                worksheet.Cells[1, 5].Value = "SĐT";
                worksheet.Cells[1, 6].Value = "Email";
                worksheet.Cells[1, 7].Value = "Mã Vé";
                worksheet.Cells[1, 8].Value = "Số Lượng";
                worksheet.Cells[1, 9].Value = "Giá Tiền";
                int row = 2;
                foreach (var hd in listHoaDon)
                {
                    // add dữ liệu tương ứng
                    worksheet.Cells[row, 1].Value = hd.HOADON.maHD;
                    worksheet.Cells[row, 2].Value = hd.HOADON.maKH;
                    worksheet.Cells[row, 3].Value = hd.HOADON.maNV;
                    worksheet.Cells[row, 3].Value = hd.HOADON.maTrangThai;
                    worksheet.Cells[row, 4].Value = hd.HOADON.ngayThanhToan;
                    worksheet.Cells[row, 5].Value = hd.HOADON.SDT;
                    worksheet.Cells[row, 6].Value = hd.HOADON.email;
                    worksheet.Cells[row, 7].Value = hd.CTHD.maVe;
                    worksheet.Cells[row, 8].Value = hd.CTHD.soLuong;
                    worksheet.Cells[row, 9].Value = hd.CTHD.giaTien;
                    row++;
                }

                // Lưu package thành file Excel
                var stream = new MemoryStream(package.GetAsByteArray());
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "HoaDons.xlsx";

                // Trả về file Excel để tải xuống
                return File(stream, contentType, fileName);
            }
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
