using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannersController(DataDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Admin/Banners")]
        public async Task<IActionResult> Index()
        {
            var listBannerVM = await (from b in _context.Banner
                              select new BannerVM
                              {
                                  Id = b.Id,
                                  TieuDe = b.TieuDe,
                                  UrlLink = b.UrlLink,
                                  isActive = b.isActive,
                              }).ToListAsync();

            return View(listBannerVM);
        }

        [HttpGet("Admin/Banners/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BannerVM bannerVM = new BannerVM();
            Banner banner = _context.Banner.FirstOrDefault(x => x.Id == id);
            bannerVM.Id = banner.Id;
            bannerVM.TieuDe = banner.TieuDe;
            bannerVM.UrlLink = banner.UrlLink;
            bannerVM.isActive = banner.isActive;
                
            if (banner == null)
            {
                return NotFound();
            }

            return View(bannerVM);
        }

        [HttpGet("Admin/Banners/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Admin/Banners/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerVM bannerVM)
        {
            if (ModelState.IsValid)
            {
                var stringImage = Uploadfile(bannerVM);
                Banner banner = new Banner() {
                    Id = bannerVM.Id,
                   TieuDe = bannerVM.TieuDe,
                   UrlLink = stringImage,
                   isActive = bannerVM.isActive 
                };
                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerVM);
        }

        [HttpGet("Admin/Banners/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BannerVM bannerVM = new BannerVM();
            Banner banner = _context.Banner.FirstOrDefault(x => x.Id == id);
            bannerVM.Id = banner.Id;
            bannerVM.TieuDe = banner.TieuDe;
            bannerVM.UrlLink = banner.UrlLink;
            bannerVM.isActive = banner.isActive;
            if (banner == null)
            {
                return NotFound();
            }
            return View(bannerVM);
        }

        [HttpPost("Admin/Banners/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BannerVM bannerVM)
        {
            if (id != bannerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var stringImage = Uploadfile(bannerVM);
                try
                {
                    Banner banner = _context.Banner.FirstOrDefault(x => x.Id == id);
                    banner.Id = bannerVM.Id;
                    banner.UrlLink = stringImage;
                    banner.TieuDe = bannerVM.TieuDe;
                    banner.isActive = bannerVM.isActive;


                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(bannerVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bannerVM);
        }

        [HttpGet("Admin/Banners/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BannerVM bannerVM = new BannerVM();
            Banner banner = _context.Banner.FirstOrDefault(x => x.Id == id);
            bannerVM.Id = banner.Id;
            bannerVM.TieuDe = banner.TieuDe;
            bannerVM.UrlLink = banner.UrlLink;
            bannerVM.isActive = banner.isActive;
            if (banner == null)
            {
                return NotFound();
            }

            return View(bannerVM);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banner.FindAsync(id);
            _context.Banner.Remove(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Banner.Any(e => e.Id == id);
        }
        private string Uploadfile(BannerVM model)
        {
            try
            {
                string filename = null;
                if (model.ImageURL != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/banner");
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
