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
    public interface IGioHangDTOService
    {
        IEnumerable<GioHangDTO> GetAll();
        /*GioHang GetGioHang(int id);
        void InsertGioHang(GioHang gioHang);
        void DeleteGioHang(int id);
        void UpdateGioHang(GioHang gioHang);*/
    }
    public class GioHangDTOService : IGioHangDTOService
    {
        private readonly DataDbContext _context;
        public GioHangDTOService(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GioHangDTO> GetAll()
        {
                var list = (from GioHang in _context.GioHang
                            join SanPham in _context.SanPham on GioHang.SanPhamId equals SanPham.Id
                            join Users in _context.Users on GioHang.UserId equals Users.Id
                            select new GioHangDTO
                            {
                                Id = GioHang.Id,
                                SoLuong = GioHang.SoLuong,
                                TongTien = GioHang.TongTien,
                                NgayTao = Users.NgayTao,
                                sanPham = SanPham,
                                user = Users
                            }).ToList();
                return list;
        }
    }
}
