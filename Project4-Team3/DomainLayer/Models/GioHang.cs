using DomainLayer.DTO;
using System;

namespace DomainLayer.Models
{
    public class GioHang : BaseEntity
    {
        public GioHang(int sanPhamId, int userId, int soLuong, DateTime? ngayTao)
        {
            SanPhamId = sanPhamId;
            UserId = userId;
            SoLuong = soLuong;
            NgayTao = ngayTao;
        }
        public GioHang(GioHangDTO gioHang)
        {
            this.SanPhamId = gioHang.SanPhamId;
            this.UserId = gioHang.UserId;
            this.SoLuong = gioHang.SoLuong;
            this.NgayTao = gioHang.NgayTao;
        }
       
        public int SanPhamId { get; set; }
        public int UserId { get; set; }
        public int SoLuong { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
