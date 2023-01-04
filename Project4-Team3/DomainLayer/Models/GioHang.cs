using DomainLayer.DTO;
using System;

namespace DomainLayer.Models
{
    public class GioHang : BaseEntity
    {
        public int SanPhamId { get; set; }
        public int UserId { get; set; }
        public int SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public DateTime? NgayTao { get; set; }
		public void SetNgayTao()
		{
			this.NgayTao = DateTime.Now;
		}
	}
}
