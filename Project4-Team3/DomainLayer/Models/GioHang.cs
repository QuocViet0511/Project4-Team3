using System;

namespace DomainLayer.Models
{
    public class GioHang : BaseEntity
    {
        public int SanPhamId { get; set; }
        public int UserId { get; set; }
        public String SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
