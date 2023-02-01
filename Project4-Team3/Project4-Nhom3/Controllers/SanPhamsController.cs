using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;

namespace Project4_Nhom3.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly DataDbContext _context;

        public SanPhamsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("SanPhams/{id}")]
        public async Task<IActionResult> Index(int? id)
        {
            decimal thanhTien;
            if (id == null)
            {
                return NotFound();
            }


            SanPhamVM sanPhamVM = new SanPhamVM();
            SanPham sanPham = _context.SanPham.FirstOrDefault(x => x.Id == id);
            KeySP keySP = _context.KeySP.FirstOrDefault(x => x.Id == sanPham.KeySPId);
            GiamGia GiamGia = _context.GiamGia.FirstOrDefault(x => x.Id == sanPham.GiamGiaId);
            DanhMucSanPham dmsp = _context.DanhMucSanPham.FirstOrDefault(x => x.Id == sanPham.DanhMucSanPhamId);

            sanPhamVM.Id = sanPham.Id;
            sanPhamVM.TenKey = keySP.KeyName;
            sanPhamVM.TenDanhMucSanPham = dmsp.TenDanhMuc;
            sanPhamVM.MaGiamGia = GiamGia.Name;
            sanPhamVM.TenSanPham = sanPham.TenSanPham;
            sanPhamVM.ThongTin = sanPham.ThongTin;
            sanPhamVM.giamGia = GiamGia;
            var checkgiamgia = GiamGia.TrangThai;
            if (checkgiamgia)
            {
                thanhTien = (decimal)(sanPham.GiaTien * (decimal)(GiamGia.PhanTramGiamGia / 100));

            }
            else
            {
                thanhTien = (decimal)sanPham.GiaTien;
            }
            sanPhamVM.GiaThayDoi = Decimal.Round(thanhTien, 1);
            sanPhamVM.GiaTien = sanPham.GiaTien;
            sanPhamVM.Image = sanPham.Image;
            sanPhamVM.NgayTao = sanPham.NgayTao;
            sanPhamVM.NgaySua = sanPham.NgaySua;
            sanPhamVM.RollNo = sanPham.RollNo;
            sanPhamVM.KeySPId = sanPham.KeySPId;
            sanPhamVM.UrlVideo = sanPham.UrlVideo;
            sanPhamVM.DanhMucSanPhamId = sanPham.DanhMucSanPhamId;
            sanPhamVM.GiamGiaId = sanPham.GiamGiaId;
            sanPhamVM.listDMSP = _context.DanhMucSanPham.Where(x => x.Id == sanPham.DanhMucSanPhamId).ToList();
            sanPhamVM.listGiamGia = _context.GiamGia.Where(x => x.Id == sanPham.GiamGiaId).ToList();
            sanPhamVM.listKeySP = _context.KeySP.Where(x => x.Id == sanPham.KeySPId).ToList();
            
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPhamVM);
        }

        [HttpPost]
        public async Task<IActionResult> ThemGioHang(List<SanPhamVM> listSPVM)
        {


            return View(listSPVM);
        }
        [HttpPost]
        public async Task<IActionResult> MuaNgay(int? id)
        {


            return View();
        }
    }
}
