using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QLVinpearl_63130803.Models
{
    public class LoginAdminModel_63130803
    {
        [DisplayName("Tài khoản Email")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        public string MKhau { get; set; }
    }
}