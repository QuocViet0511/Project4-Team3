using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

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

        // GET: Admin/Banners
        [HttpGet("Admin/Banners")]
        public async Task<IActionResult> Index()
        {
            var listBanners = (from bn in _context.Banner

                               select new Banner
                               {
                                   Id = bn.Id,
                                   TieuDe = bn.TieuDe,
                                   UrlLink = bn.UrlLink,
                                   isActive = bn.isActive,
                               }).AsEnumerable().ToList();


            return View(listBanners);
        }

        // GET: Admin/Banners/Details/5
        [HttpGet("Admin/Banners/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Banner banner = new Banner();

            banner.Id = banner.Id;
            banner.TieuDe = banner.TieuDe;
            banner.UrlLink = banner.UrlLink;
            banner.isActive = banner.isActive;

            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Admin/Banners/Create
        [HttpGet("Admin/Banners/Create")]
        public IActionResult Create()
        {
            var listBanners = _context.Banner.AsEnumerable().ToList();
            ViewBag.BN = new SelectList(listBanners, "Id", "TieuDe");
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner, IFormFile UrlLink)
        {
            banner.UrlLink = UrlLink;
            var stringImage = Uploadfile(banner);
            if (ModelState.IsValid)
            {

                Banner banners = new Banner()
                {
                    Id = banner.Id,
                    TieuDe = banner.TieuDe,
                    UrlLink = stringImage,
                    isActive = banner.isActive,
                };
                _context.Add(banner);
                await _context.SaveChangesAsync();
                return Redirect("~/Admin/Banners");
            }
            return View(banner);
        }

        // GET: Admin/Banners/Edit/5
        [HttpGet("Admin/Banners/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Banner banner = new Banner();

            banner.Id = banner.Id;
            banner.TieuDe = banner.TieuDe;
            banner.UrlLink = banner.UrlLink;
            banner.isActive = banner.isActive;

            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var stringImage = Uploadfile(banner);
                try
                {
                    Banner banner1 = _context.Banner.FirstOrDefault(x => x.Id == id);
                    banner.Id = banner.Id;
                    banner.TieuDe = banner.TieuDe;
                    banner.UrlLink = banner.UrlLink;
                    banner.isActive = banner.isActive;

                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Admin/Banners");
            }
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5
        [HttpGet("Admin/Banners/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Banner banner = new Banner();

            banner.Id = banner.Id;
            banner.TieuDe = banner.TieuDe;
            banner.UrlLink = banner.UrlLink;
            banner.isActive = banner.isActive;

            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banner.FindAsync(id);
            _context.Banner.Remove(banner);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/Banners");
        }

        private bool BannerExists(int id)
        {
            return _context.Banner.Any(e => e.Id == id);
        }
        private string Uploadfile(Banner model)
        {
            try
            {
                string filename = null;
                if (model.UrlLink != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/anhsanpham");
                    filename = model.UrlLink;
                    string filepath = Path.Combine(uploadDir, filename);
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        model.UrlLink.CopyTo(fileStream);
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
