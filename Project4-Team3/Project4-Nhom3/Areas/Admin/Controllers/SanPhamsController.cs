using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Service;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SanPhamsController(DataDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/SanPhams
        [HttpGet("Admin/SanPhams")]
        public async Task<IActionResult> Index()
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
                                UrlVideo = sp.UrlVideo,
                                listDMSP = _context.DanhMucSanPham.ToList(),
                                listGiamGia = _context.GiamGia.ToList(),
                                listKeySP = _context.KeySP.ToList(),
                            }).AsEnumerable().ToList();


            return View(listSPVM);
        }

        // GET: Admin/SanPhams/Details/5
        [HttpGet("Admin/SanPhams/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            SanPhamVM sanPhamVM = new SanPhamVM();
            SanPham sanPham = _context.SanPham.FirstOrDefault(x => x.Id == id);
            KeySP keySP = _context.KeySP.FirstOrDefault(x => x.Id == sanPham.KeySPId);
            GiamGia giamGia = _context.GiamGia.FirstOrDefault(x => x.Id == sanPham.GiamGiaId);
            DanhMucSanPham dmsp = _context.DanhMucSanPham.FirstOrDefault(x => x.Id == sanPham.DanhMucSanPhamId);

            sanPhamVM.Id = sanPham.Id;
            sanPhamVM.TenKey = keySP.KeyName;
            sanPhamVM.TenDanhMucSanPham = dmsp.TenDanhMuc;
            sanPhamVM.MaGiamGia = giamGia.Name;
            sanPhamVM.TenSanPham = sanPham.TenSanPham;
            sanPhamVM.ThongTin = sanPham.ThongTin;
            sanPhamVM.GiaTien = sanPham.GiaTien;
            sanPhamVM.Image = sanPham.Image;
            sanPhamVM.NgayTao = sanPham.NgayTao;
            sanPhamVM.NgaySua = sanPham.NgaySua;
            sanPhamVM.RollNo = sanPham.RollNo;
            sanPhamVM.KeySPId = sanPham.KeySPId;
            sanPhamVM.DanhMucSanPhamId = sanPham.DanhMucSanPhamId;
            sanPhamVM.GiamGiaId = sanPham.GiamGiaId;
            sanPhamVM.UrlVideo = sanPham.UrlVideo;
            sanPhamVM.listDMSP = _context.DanhMucSanPham.Where(x => x.Id == sanPham.DanhMucSanPhamId).ToList();
            sanPhamVM.listGiamGia = _context.GiamGia.Where(x => x.Id == sanPham.GiamGiaId).ToList();
            sanPhamVM.listKeySP = _context.KeySP.Where(x => x.Id == sanPham.KeySPId).ToList();
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPhamVM);
        }

        // GET: Admin/SanPhams/Create
        [HttpGet("Admin/SanPhams/Create")]
        public IActionResult Create()
        {
            var listDMSP = _context.DanhMucSanPham.AsEnumerable().ToList();
            var listGiamGia = _context.GiamGia.AsEnumerable().ToList();
            var listKeySP = _context.KeySP.AsEnumerable().ToList();
            ViewBag.DMSP = new SelectList(listDMSP, "Id", "TenDanhMuc");
            ViewBag.GG = new SelectList(listGiamGia, "Id", "Name");
            ViewBag.KeySP = new SelectList(listKeySP, "Id", "KeyName");
            return View();
        }

        // POST: Admin/SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPhamVM sanPhamVM, IFormFile ImageURL)
        {
            sanPhamVM.ImageURL = ImageURL;
            var stringImage = Uploadfile(sanPhamVM);
            if (ModelState.IsValid)
            {

                SanPham sanPham = new SanPham()
                {
                    Id = sanPhamVM.Id,
                    DanhMucSanPhamId = sanPhamVM.DanhMucSanPhamId,
                    GiamGiaId = sanPhamVM.GiamGiaId,
                    KeySPId = sanPhamVM.KeySPId,
                    GiaTien = sanPhamVM.GiaTien,
                    Image = stringImage,
                    NgayTao = sanPhamVM.NgayTao,
                    NgaySua = sanPhamVM.NgaySua,
                    RollNo = sanPhamVM.RollNo,
                    TenSanPham = sanPhamVM.TenSanPham,
                    ThongTin = sanPhamVM.ThongTin,
                    UrlVideo = sanPhamVM.UrlVideo.Replace("watch?v=", "embed/")
                };
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return Redirect("~/Admin/SanPhams");
            }
            return View(sanPhamVM);
        }

        // GET: Admin/SanPhams/Edit/5
        [HttpGet("Admin/SanPhams/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SanPhamVM sanPhamVM = new SanPhamVM();
            SanPham sanPham = _context.SanPham.FirstOrDefault(x => x.Id == id);
            KeySP keySP = _context.KeySP.FirstOrDefault(x => x.Id == sanPham.KeySPId);
            GiamGia giamGia = _context.GiamGia.FirstOrDefault(x => x.Id == sanPham.GiamGiaId);
            DanhMucSanPham dmsp = _context.DanhMucSanPham.FirstOrDefault(x => x.Id == sanPham.DanhMucSanPhamId);

            sanPhamVM.Id = sanPham.Id;
            sanPhamVM.TenKey = keySP.KeyName;
            sanPhamVM.TenDanhMucSanPham = dmsp.TenDanhMuc;
            sanPhamVM.MaGiamGia = giamGia.Name;
            sanPhamVM.TenSanPham = sanPham.TenSanPham;
            sanPhamVM.ThongTin = sanPham.ThongTin;
            sanPhamVM.GiaTien = sanPham.GiaTien;
            sanPhamVM.Image = sanPham.Image;
            sanPhamVM.NgayTao = sanPham.NgayTao;
            sanPhamVM.NgaySua = sanPham.NgaySua;
            sanPhamVM.RollNo = sanPham.RollNo;
            sanPhamVM.UrlVideo = sanPham.UrlVideo;
            sanPhamVM.listDMSP = _context.DanhMucSanPham.Where(x => x.Id == sanPham.DanhMucSanPhamId).ToList();
            sanPhamVM.listGiamGia = _context.GiamGia.Where(x => x.Id == sanPham.GiamGiaId).ToList();
            sanPhamVM.listKeySP = _context.KeySP.Where(x => x.Id == sanPham.KeySPId).ToList();

            var listDMSP = _context.DanhMucSanPham.AsEnumerable().ToList();
            var listGiamGia = _context.GiamGia.AsEnumerable().ToList();
            var listKeySP = _context.KeySP.AsEnumerable().ToList();
            ViewBag.DMSP = new SelectList(listDMSP, "Id", "TenDanhMuc", sanPham.DanhMucSanPhamId);
            ViewBag.GG = new SelectList(listGiamGia, "Id", "Name", sanPham.GiamGiaId);
            ViewBag.KeySP = new SelectList(listKeySP, "Id", "KeyName", sanPham.KeySPId);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPhamVM);
        }

        // POST: Admin/SanPhams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPhamVM sanPhamVM)
        {
            string stringImage;
            if (id != sanPhamVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                

                try
                {
                    SanPham sanPham = _context.SanPham.FirstOrDefault(x => x.Id == id);
                    sanPham.Id = sanPhamVM.Id;
                    sanPham.DanhMucSanPhamId = sanPhamVM.DanhMucSanPhamId;
                    sanPham.GiamGiaId = sanPhamVM.GiamGiaId;
                    sanPham.KeySPId = sanPhamVM.KeySPId;
                    sanPham.GiaTien = sanPhamVM.GiaTien;
                    if (sanPhamVM.Image != null)
                    {
                        stringImage = Uploadfile(sanPhamVM);
                    }
                    else
                    {
                        stringImage = sanPham.Image;
                    }
                    sanPham.Image = stringImage;
                    sanPham.NgayTao = sanPhamVM.NgayTao;
                    sanPham.NgaySua = sanPhamVM.NgaySua;
                    sanPham.RollNo = sanPhamVM.RollNo;
                    sanPham.TenSanPham = sanPhamVM.TenSanPham;
                    sanPham.ThongTin = sanPhamVM.ThongTin;
                    sanPham.UrlVideo = sanPhamVM.UrlVideo.Replace("watch?v=", "embed/");
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPhamVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Admin/SanPhams");
            }
            return View(sanPhamVM);
        }

        // GET: Admin/SanPhams/Delete/5
        [HttpGet("Admin/SanPhams/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SanPhamVM sanPhamVM = new SanPhamVM();
            SanPham sanPham = _context.SanPham.FirstOrDefault(x => x.Id == id);
            KeySP keySP = _context.KeySP.FirstOrDefault(x => x.Id == sanPham.KeySPId);
            GiamGia giamGia = _context.GiamGia.FirstOrDefault(x => x.Id == sanPham.GiamGiaId);
            DanhMucSanPham dmsp = _context.DanhMucSanPham.FirstOrDefault(x => x.Id == sanPham.DanhMucSanPhamId);

            sanPhamVM.Id = sanPham.Id;
            sanPhamVM.TenKey = keySP.KeyName;
            sanPhamVM.TenDanhMucSanPham = dmsp.TenDanhMuc;
            sanPhamVM.MaGiamGia = giamGia.Name;
            sanPhamVM.TenSanPham = sanPham.TenSanPham;
            sanPhamVM.ThongTin = sanPham.ThongTin;
            sanPhamVM.GiaTien = sanPham.GiaTien;
            sanPhamVM.Image = sanPham.Image;
            sanPhamVM.NgayTao = sanPham.NgayTao;
            sanPhamVM.NgaySua = sanPham.NgaySua;
            sanPhamVM.RollNo = sanPham.RollNo;
            sanPhamVM.UrlVideo = sanPham.UrlVideo;
            sanPhamVM.listDMSP = _context.DanhMucSanPham.Where(x => x.Id == sanPham.DanhMucSanPhamId).ToList();
            sanPhamVM.listGiamGia = _context.GiamGia.Where(x => x.Id == sanPham.GiamGiaId).ToList();
            sanPhamVM.listKeySP = _context.KeySP.Where(x => x.Id == sanPham.KeySPId).ToList();
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPhamVM);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/SanPhams");
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.Id == id);
        }
        private string Uploadfile(SanPhamVM model)
        {
            try
            {
                string filename = null;
                if (model.ImageURL != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/anhsanpham");
                    filename = model.ImageURL.FileName;
                    string filepath = Path.Combine(uploadDir, filename);
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        model.ImageURL.CopyTo(fileStream);
                    }
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return filename;
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return null;
            }
        }
    }
}
