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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.SOCAs = new HashSet<SOCA>();
        }
    
        public string maNV { get; set; }
        public string maLoaiNV { get; set; }
        public string hoTenNV { get; set; }
        public string diaChi { get; set; }
        public Nullable<System.DateTime> ngaySinh { get; set; }
        public string sdt { get; set; }
        public Nullable<bool> gioiTinh { get; set; }
        public string anh { get; set; }
        public string email { get; set; }
        public string matKhau { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public virtual LOAINV LOAINV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOCA> SOCAs { get; set; }
    }
}