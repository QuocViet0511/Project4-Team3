using DomainLayer.DTO;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
	internal class GioHangDTOService
	{
		private readonly DataDbContext _context;
		public readonly DbSet<GioHang> _gioHang;
		public readonly DbSet<SanPham> _sanPham;
 		public GioHangDTOService(DataDbContext context)
		{
			_context = context;
			_gioHang = context.Set<GioHang>();
			_sanPham = context.Set<SanPham>();
		}
		 
		public IEnumerable<GioHangDTO> GetAll()
		{
			var list = (from GioHang in _gioHang.ToList()
						join SanPham in _sanPham.ToList() on GioHang.SanPhamId equals SanPham.Id
						select new GioHangDTO
						{
							Id= GioHang.Id,
							Image= SanPham.Image,
							Name= SanPham.TenSanPham,
							DonGia= SanPham.GiaTien,
							SoLuong= GioHang.SoLuong,
							TongTien= GioHang.TongTien
						}).tolist();

			return null;
		}
	}
}
