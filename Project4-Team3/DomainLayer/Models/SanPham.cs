using System;

namespace DomainLayer.Models
{
    public class SanPham : BaseEntity
    {
        public string RollNo { get; set; }
        public int DanhMucSanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public string ThongTin { get; set; }
        public string Image { get; set; }
        public decimal? GiaTien { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public int GiamGiaId { get; set; }
        public int KeySPId { get; set; }
    }
}
