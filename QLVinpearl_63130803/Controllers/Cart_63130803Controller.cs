using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVinpearl_63130803.Models;
using System.Web.Helpers;
using QLVinpearl_63130803.Other;
using System.Configuration;

namespace QLVinpearl_63130803.Controllers
{
    public class Cart_63130803Controller : Controller
    {
        private QLVinpearl_63130803Entities db = new QLVinpearl_63130803Entities();
        // GET: Cart_63130803
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult PlusItem(string maDV)
		{
			var cart = Session["cart"] as List<Dictionary<string, object>>;
			var item = cart.FirstOrDefault(p => p["maDV"].ToString() == maDV);
			if (item != null)
			{
				// Sửa thông tin của sản phẩm
				item["soLuong"] = Convert.ToInt32(item["soLuong"]) + 1;
				int soLuongItem = Convert.ToInt32(item["soLuong"]);
				int gia = Convert.ToInt32(item["gia"]);
				item["thanhTien"] = soLuongItem * gia;
				// Cập nhật lại session
				Session["cart"] = cart;
			}
			return RedirectToAction("Index");
		}
		public ActionResult MinusItem(string maDV)
		{
			var cart = Session["cart"] as List<Dictionary<string, object>>;
			var item = cart.FirstOrDefault(p => p["maDV"].ToString() == maDV);
			if (item != null)
			{
				// Sửa thông tin của sản phẩm
				var soLuong = Convert.ToInt32(item["soLuong"]);
				if (soLuong == 1)
				{
					item["soLuong"] = 1;
					Session["cart"] = cart;
					return RedirectToAction("Index");
				}
				item["soLuong"] = Convert.ToInt32(item["soLuong"]) - 1;
				int soLuongItem = Convert.ToInt32(item["soLuong"]);
				int gia = Convert.ToInt32(item["gia"]);
				item["thanhTien"] = soLuongItem * gia;
				// Cập nhật lại session
				Session["cart"] = cart;
			}
			return RedirectToAction("Index");
		}
		public ActionResult RemoveItem(string maDV)
		{
			var cart = Session["cart"] as List<Dictionary<string, object>>;

			if (cart != null)
			{
				cart.RemoveAll(item => item["maDV"].ToString() == maDV);
				Session["cart"] = cart;
			}
			return RedirectToAction("Index");
		}
		public ActionResult Cart(string id, int loaiVe)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			DICHVU dICHVU = db.DICHVUs.Find(id);
			if (dICHVU == null)
			{
				return HttpNotFound();
			}

			string emailKH = Session["EmailKH"].ToString();

			var cart = Session["cart"] as List<Dictionary<string, object>>;

			if (cart != null)
			{
				// Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
				var existingItem = cart.FirstOrDefault(item => item.ContainsKey("maDV") && item["maDV"].ToString() == dICHVU.maDV);
				if (existingItem != null)
				{
					// Nếu sản phẩm đã tồn tại, chỉ cần cập nhật thông tin mới
					existingItem["loaiVe"] = loaiVe;
					existingItem["soLuong"] = Convert.ToInt32(existingItem["soLuong"]) + 1;
				}
				else
				{
					// Nếu sản phẩm chưa tồn tại, thêm mới vào giỏ hàng
					bool boolValue = Convert.ToBoolean(loaiVe);
					var query = (from ve in db.VEs
								 join dv in db.DICHVUs on ve.maDV equals dv.maDV
								 where dv.maDV == id && ve.loaiVe == boolValue
								 select new
								 {
									 Ve = ve,
									 DichVu = dv
								 }).SingleOrDefault();
					object thanhTien = null;
					var cartItem = new Dictionary<string, object>
					{
						{ "maDV", dICHVU.maDV },
						{ "loaiVe", loaiVe },
						{ "soLuong", 1 },
						{ "anh", query.DichVu.anh },
						{"tenDV", query.DichVu.tenDV },
						{"gia", query.Ve.giaTien },
						{ "thanhTien", thanhTien }
					};
					int soLuongItem = Convert.ToInt32(cartItem["soLuong"]);
					int gia = Convert.ToInt32(cartItem["gia"]);
					cartItem["thanhTien"] = soLuongItem * gia;
					cart.Add(cartItem);
				}
			}
			else
			{
				// Nếu giỏ hàng chưa tồn tại, tạo mới và thêm sản phẩm vào
				bool boolValue = Convert.ToBoolean(loaiVe);
				var query = (from ve in db.VEs
							 join dv in db.DICHVUs on ve.maDV equals dv.maDV
							 where dv.maDV == id && ve.loaiVe == boolValue
							 select new
							 {
								 Ve = ve,
								 DichVu = dv
							 }).SingleOrDefault();
				object thanhTien = null;
				var cartItem = new Dictionary<string, object>
				{
					{ "maDV", dICHVU.maDV },
					{ "loaiVe", loaiVe },
					{ "soLuong", 1 },
					{ "anh", query.DichVu.anh },
					{"tenDV", query.DichVu.tenDV },
					{"gia", query.Ve.giaTien },
					{ "thanhTien", thanhTien }
		};
				int soLuongItem = Convert.ToInt32(cartItem["soLuong"]);
				int gia = Convert.ToInt32(cartItem["gia"]);
				cartItem["thanhTien"] = soLuongItem * gia;
				cart = new List<Dictionary<string, object>> { cartItem };
			}
			Session["cart"] = cart;
			return RedirectToAction("Index");
		}
		string LayMaHD()
		{
			var maMax = db.HOADONs.ToList().Select(n => n.maHD).Max();
			int maHD = int.Parse(maMax.Substring(2)) + 1;
			string HD = maHD.ToString().PadLeft(6, '0');
			return "HD" + HD;
		}
		public ActionResult Buy()
		{
			string maHD = LayMaHD();
			string email = Session["EmailKH"].ToString();
			var user = db.KHACHHANGs.FirstOrDefault(kh => kh.email == email);
			var newHoaDon = new HOADON
			{
				maHD = maHD,
				maKH = user.maKH,
				maNV = "NV001",
				ngayThanhToan = DateTime.Now,
				SDT = user.SDT,
				email = user.email
			};
			db.HOADONs.Add(newHoaDon);
			db.SaveChanges();

			var cart = Session["cart"] as List<Dictionary<string, object>>;

			decimal totalPrice = 0;

			foreach (var cartItem in cart)
			{
				string maDV = cartItem["maDV"].ToString();
				bool boolValue = Convert.ToBoolean(cartItem["loaiVe"]);
				var query = (from ve in db.VEs
							 join dv in db.DICHVUs on ve.maDV equals dv.maDV
							 where dv.maDV == maDV && ve.loaiVe == boolValue
							 select new
							 {
								 Ve = ve,
								 DichVu = dv
							 }).SingleOrDefault();

				var newCTHD = new CTHD
				{
					maHD = maHD,
					maVe = query.Ve.maVe,
					soLuong = Convert.ToInt32(cartItem["soLuong"]),
					giaTien = Convert.ToInt32(cartItem["thanhTien"])

				};
				db.CTHDs.Add(newCTHD);
				db.SaveChanges();

				totalPrice += Convert.ToDecimal(cartItem["thanhTien"]);
			}

			System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
			mail.From = new System.Net.Mail.MailAddress("nga.vm.63cntt@ntu.edu.vn");
			mail.To.Add(Session["EmailKH"].ToString());
			mail.Subject = "Hoá Đơn Thanh Toán Của Bạn!";
			string body = "<html><body>";
			body += "<h2>Đây là thông tin chi tiết về dịch vụ mà bạn đã mua:</h2>";
			body += "<ul>";

			foreach (var cartItem in cart)
			{
				string loaiVe = (cartItem["loaiVe"] as int?) == 0 ? "Trẻ Em" : "Người Lớn";
				body += "<li><strong>Tên dịch vụ:</strong> " + cartItem["tenDV"] + " | <strong>Loại Vé:</strong> " + loaiVe + " | <strong>Số Lượng:</strong> " + cartItem["soLuong"] + "</li>";
			}

			body += "</ul>";
			body += "<p><strong>Ngày Mua:</strong> " + DateTime.Now + "</p>";
			//decimal totalPrice = Convert.ToDecimal(Session["tongTienMua"]);
			//string formattedPrice = totalPrice.ToString("#,##0 VNĐ");
			string formattedAmount = (totalPrice * 100).ToString();

			// Add the formatted price to the email body
			body += "<p><strong>Tổng Tiền:</strong> " + formattedAmount + "</p>";

			//body += "<p><strong>Tổng Tiền:</strong> " + Session["tongTienMua"] + " vnđ</p>";
			body += "</body></html>";

			mail.Body = body;
			mail.IsBodyHtml = true;
			System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
			smtp.Credentials = new System.Net.NetworkCredential("nga.vm.63cntt@ntu.edu.vn", "225935367@@");
			smtp.Port = 587;
			smtp.EnableSsl = true;
			smtp.Send(mail);

			//Session["cart"] = null;
			//Session["tongTienMua"] = null;
			//return RedirectToAction("Index");
			string url = ConfigurationManager.AppSettings["Url"];
			string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
			string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
			string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

			PayLib pay = new PayLib();

			pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
			pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
			pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
			//pay.AddRequestData("vnp_Amount", "1000000"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
			pay.AddRequestData("vnp_Amount", formattedAmount);
			pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
			pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
			pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
			pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
			pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
			pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
			pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
			pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
			pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

			string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

			Session["cart"] = null;
			Session["tongTienMua"] = null;

			return Redirect(paymentUrl);
		}
		public ActionResult PaymentConfirm()
		{
			if (Request.QueryString.Count > 0)
			{
				string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
				var vnpayData = Request.QueryString;
				PayLib pay = new PayLib();

				//lấy toàn bộ dữ liệu được trả về
				foreach (string s in vnpayData)
				{
					if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
					{
						pay.AddResponseData(s, vnpayData[s]);
					}
				}

				long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
				long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
				string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
				string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

				bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

				if (checkSignature)
				{
					if (vnp_ResponseCode == "00")
					{
						//Thanh toán thành công
						ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
					}
					else
					{
						//Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
						ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
					}
				}
				else
				{
					ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
				}
			}

			return View();
		}
	}
}