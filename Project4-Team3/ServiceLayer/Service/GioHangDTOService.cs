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
        GioHangDTO GetGioHangDTO(int? id);
        /*void InsertGioHang(GioHang gioHang);
        void DeleteGioHang(int id);*/
        void UpdateGioHangDTO(GioHangDTO gioHang);
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
                                NgayTao = Users.NgayTao,
                                sanPham = SanPham,
                                user = Users
                            }).ToList();
                return list;
        }

        public GioHangDTO GetGioHangDTO(int? id)
        {
            var gioHang = (from GioHang in _context.GioHang where GioHang.Id == id
                        join SanPham in _context.SanPham on GioHang.SanPhamId equals SanPham.Id
                        join Users in _context.Users on GioHang.UserId equals Users.Id
                        select new GioHangDTO
                        {
                            Id = GioHang.Id,
                            SoLuong = GioHang.SoLuong,
                            NgayTao = Users.NgayTao,
                            TongTien = GioHang.TongTien,
                            sanPham = SanPham,
                            user = Users
                        }).First();
            return gioHang;
        }

        public void UpdateGioHangDTO(GioHangDTO gioHang)
        {
            var _gioHang = new GioHang()
            {
                Id = gioHang.Id,
                SoLuong = gioHang.SoLuong,
                NgayTao = gioHang.NgayTao,
                TongTien = gioHang.TongTien
            };
            _context.Update(_gioHang);
            _context.SaveChanges();
        }
    }
}
