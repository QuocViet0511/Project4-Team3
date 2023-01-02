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
    public class GiamGiasController : Controller
    {
        private readonly DataDbContext _context;

        public GiamGiasController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GiamGias
        [HttpGet("Admin/GiamGias")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GiamGia.ToListAsync());
        }

        // GET: Admin/GiamGias/Details/5
        [HttpGet("Admin/GiamGias/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGia = await _context.GiamGia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giamGia == null)
            {
                return NotFound();
            }

            return View(giamGia);
        }

        // GET: Admin/GiamGias/Create
        [HttpGet("Admin/GiamGias/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GiamGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ThongTin,PhanTramGiamGia,TrangThai,NgayTao,NgaySua,Id")] GiamGia giamGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giamGia);
                await _context.SaveChangesAsync();
                return Redirect("~/Admin/GiamGias");
            }
            return View(giamGia);
        }

        // GET: Admin/GiamGias/Edit/5
        [HttpGet("Admin/GiamGias/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGia = await _context.GiamGia.FindAsync(id);
            if (giamGia == null)
            {
                return NotFound();
            }
            return View(giamGia);
        }

        // POST: Admin/GiamGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ThongTin,PhanTramGiamGia,TrangThai,NgayTao,NgaySua,Id")] GiamGia giamGia)
        {
            if (id != giamGia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giamGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiamGiaExists(giamGia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Admin/GiamGias");
            }
            return View(giamGia);
        }

        // GET: Admin/GiamGias/Delete/5
        [HttpGet("Admin/GiamGias/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giamGia = await _context.GiamGia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giamGia == null)
            {
                return NotFound();
            }

            return View(giamGia);
        }

        // POST: Admin/GiamGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giamGia = await _context.GiamGia.FindAsync(id);
            _context.GiamGia.Remove(giamGia);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/GiamGias");
        }

        private bool GiamGiaExists(int id)
        {
            return _context.GiamGia.Any(e => e.Id == id);
        }
    }
}
