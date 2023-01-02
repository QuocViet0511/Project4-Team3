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

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly DataDbContext _context;

        public SanPhamsController(DataDbContext context )
        {
            _context = context;
        }

        // GET: Admin/SanPhams
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var listSp = _context.SanPham.ToListAsync();
            var listKey = _context.KeySP.ToListAsync();
            var listMaGG = _context.GiamGia.ToListAsync();

            var listSPVM = (from sp in _context.SanPham
                            join k in _context.KeySP on sp.KeySPId equals k.Id
                            join mgg in _context.GiamGia on sp.GiamGiaId equals mgg.Id 
                            select new SanPhamVM {
                            Id = sp.Id,

                            }).ToListAsync();

                return View();
        }

        // GET: Admin/SanPhams/Details/5
        [HttpGet("Admin/SanPhams/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        [HttpGet("Admin/SanPhams/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RollNo,DanhMucSanPhamId,TenSanPham,ThongTin,Image,GiaTien,NgayTao,NgaySua,GiamGiaId,KeySPId,Id")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        [HttpGet("Admin/SanPhams/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RollNo,DanhMucSanPhamId,TenSanPham,ThongTin,Image,GiaTien,NgayTao,NgaySua,GiamGiaId,KeySPId,Id")] SanPham sanPham)
        {
            if (id != sanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
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
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        [HttpGet("Admin/SanPhams/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.Id == id);
        }
    }
}
