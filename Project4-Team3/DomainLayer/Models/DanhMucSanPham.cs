using System;

namespace DomainLayer.Models
{
    public class DanhMucSanPham : BaseEntity
    {
        public string TenDanhMuc { get; set; }
        public string ThongTin { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
    }
}
