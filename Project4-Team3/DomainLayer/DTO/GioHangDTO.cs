using DomainLayer.Models;
using System;

namespace DomainLayer.DTO
{
	public class GioHangDTO : GioHang
	{
        public SanPham sanPham { get; set; }
		public Users user { get; set; }
     }
}
