//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLVinpearl_63130803.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public string maKH { get; set; }
        public string hoTenKH { get; set; }
        public string SDT { get; set; }
        public string diaChi { get; set; }
        public Nullable<System.DateTime> ngaySinh { get; set; }
        public Nullable<bool> gioiTinh { get; set; }
        public string email { get; set; }
        public string matKhau { get; set; }
        public string anh { get; set; }
        public string ResetPasswordCode { get; set; }
        public Nullable<System.DateTime> ResetPasswordCodeExpiration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
