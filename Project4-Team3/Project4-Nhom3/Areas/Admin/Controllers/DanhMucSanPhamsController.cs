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
    public class DanhMucSanPhamsController : Controller
    {
        private readonly DataDbContext _context;

        public DanhMucSanPhamsController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DanhMucSanPhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucSanPham.ToListAsync());
        }

        // GET: Admin/DanhMucSanPhams/Details/5
        [Route("Detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenDanhMuc,ThongTin,NgayTao,NgaySua,Id")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Edit/5
        [Route("Admin/DanhMucSanPhams/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }
            return View(danhMucSanPham);
        }

        // POST: Admin/DanhMucSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,DanhMucSanPham danhMucSanPham)
        {
            if (id != danhMucSanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucSanPhamExists(danhMucSanPham.Id))
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
            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Delete/5
        [Route("Admin/DanhMucSanPhams/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // POST: Admin/DanhMucSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucSanPham = await _context.DanhMucSanPham.FindAsync(id);
            _context.DanhMucSanPham.Remove(danhMucSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DanhMucSanPhamExists(int id)
        {
            return _context.DanhMucSanPham.Any(e => e.Id == id);
        }
    }
}
