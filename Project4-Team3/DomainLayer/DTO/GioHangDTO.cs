using DomainLayer.Models;
using System;

namespace DomainLayer.DTO
{
	public class GioHangDTO : GioHang
	{
        public GioHangDTO(GioHangDTO gioHang) : base(gioHang)
        {
        }

        public GioHangDTO(int sanPhamId, int userId, int soLuong, DateTime? ngayTao) : base(sanPhamId, userId, soLuong, ngayTao)
        {
        }

        public SanPham sanPham { get; set; }
		public Users user { get; set; }
        public decimal? TongTien {
			get
			{
				return this.SoLuong * this.sanPham.GiaTien;
			}
		}

    }
}
