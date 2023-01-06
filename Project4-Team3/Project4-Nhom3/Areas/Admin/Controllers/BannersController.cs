using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly DataDbContext _context;

        public BannersController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Banners
        [HttpGet("Admin/Banners")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banner.ToListAsync());
        }

        // GET: Admin/Banners/Details/5
        [HttpGet("Admin/Banners/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banner
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrlLink,TieuDe,Id")] Banner banner)
        {
            if (ModelState.IsValid)
            {
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

            var banner = await _context.Banner.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("UrlLink,TieuDe,Id")] Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

            var banner = await _context.Banner
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
