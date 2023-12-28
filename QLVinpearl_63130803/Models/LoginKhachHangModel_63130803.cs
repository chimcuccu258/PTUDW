using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace QLVinpearl_63130803.Models
{
    public class LoginKhachHangModel_63130803
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        [DisplayName("Tài khoản Email")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        public string MKhau { get; set; }
    }
}