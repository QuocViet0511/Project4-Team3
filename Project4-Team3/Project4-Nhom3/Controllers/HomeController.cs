using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project4_Nhom3.Models;
using RepositoryLayer;
using ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project4_Nhom3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISanPhamService _sanPhamService;
        private readonly IDanhMucSanPhamService _danhMucSanPhamService;
        private readonly DataDbContext _context;

        public HomeController(ILogger<HomeController> logger, DataDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var listSPVM = (from sp in _context.SanPham
                            join dmsp in _context.DanhMucSanPham on sp.DanhMucSanPhamId equals dmsp.Id
                            join k in _context.KeySP on sp.KeySPId equals k.Id
                            join mgg in _context.GiamGia on sp.GiamGiaId equals mgg.Id
                            select new SanPhamVM
                            {
                                Id = sp.Id,
                                TenKey = k.KeyName,
                                TenDanhMucSanPham = dmsp.TenDanhMuc,
                                MaGiamGia = mgg.Name,
                                TenSanPham = sp.TenSanPham,
                                ThongTin = sp.ThongTin,
                                GiaTien = sp.GiaTien,
                                Image = sp.Image,
                                NgayTao = sp.NgayTao,
                                NgaySua = sp.NgaySua,
                                RollNo = sp.RollNo,
                                listDMSP = _context.DanhMucSanPham.ToList(),
                                listGiamGia = _context.GiamGia.ToList(),
                                listKeySP = _context.KeySP.ToList(),
                                listBanner = (from b in _context.Banner
                                              select new BannerVM
                                              {
                                                  Id = b.Id,
                                                  TieuDe = b.TieuDe,
                                                  UrlLink = b.UrlLink,
                                                  isActive = b.isActive,
                                              }).ToList(),
                            }).AsEnumerable().ToList();


            return View(listSPVM);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        //[HttpPost("Home/Feedback")]
        public IActionResult Feedback()
        {
            var feedback = new Feedback();
            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Feedback(Feedback feedback)
        {
            return View();

        }   
    }
}   
