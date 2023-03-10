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
using DomainLayer.DTO;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GioHangsController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IGioHangService _gioHangService;
        private readonly IGioHangDTOService _gioHangDTOService;

        public GioHangsController(DataDbContext context, IGioHangService gioHangService, IGioHangDTOService gioHangDTOService)
        {
            _context = context;
            _gioHangService = gioHangService;
            _gioHangDTOService = gioHangDTOService;
        }

        // GET: Admin/GioHangs
        [Route("Admin/GioHangs")]
        public async Task<IActionResult> Index()
        {
            return View(_gioHangDTOService.GetAll().OrderBy(x=>x.NgayTao));
        }

        // GET: Admin/GioHangs/Details/5
        [Route("Admin/GioHangs/Detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // GET: Admin/GioHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                gioHang.NgayTao = DateTime.Now;
				_gioHangService.InsertGioHang(gioHang);
                return RedirectToAction(nameof(Index));
            }
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Edit/5
        [HttpGet("Admin/GioHangs/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gioHang = _gioHangService.GetGioHang(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            return View(gioHang);
        }

        // POST: Admin/GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GioHang gioHang)
        {
            if (id != gioHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _gioHangService.UpdateGioHang(gioHang);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangExists(gioHang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Admin/GioHangs");
            }
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Delete/5
        [Route("Admin/GioHangs/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // POST: Admin/GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioHang = await _context.GioHang.FindAsync(id);
            _context.GioHang.Remove(gioHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangExists(int id)
        {
            return _context.GioHang.Any(e => e.Id == id);
        }
    }
}
