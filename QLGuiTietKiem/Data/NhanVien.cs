//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLGuiTietKiem.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.SoTietKiems = new HashSet<SoTietKiem>();
        }
    
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string MK { get; set; }
        public string LoaiNguoiDung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoTietKiem> SoTietKiems { get; set; }
    }
}
