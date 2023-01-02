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
    public class KeySPsController : Controller
    {
        private readonly DataDbContext _context;

        public KeySPsController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Admin/KeySPs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.KeySP.ToListAsync());
        }

        [HttpGet("Admin/KeySPs/Details/{id}")]
        // GET: Admin/KeySPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySP = await _context.KeySP
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keySP == null)
            {
                return NotFound();
            }

            return View(keySP);
        }

        // GET: Admin/KeySPs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/KeySPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeyName,KeyInfo,NgayTao,TrangThaiKey,Id")] KeySP keySP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keySP);
                await _context.SaveChangesAsync();
                return Redirect("~/Admin/KeySPs/Index");
            }
            return View(keySP);
        }

        // GET: Admin/KeySPs/Edit/5
        [HttpGet("Admin/KeySPs/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySP = await _context.KeySP.FindAsync(id);
            if (keySP == null)
            {
                return NotFound();
            }
            return View(keySP);
        }

        // POST: Admin/KeySPs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeyName,KeyInfo,NgayTao,TrangThaiKey,Id")] KeySP keySP)
        {
            if (id != keySP.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keySP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeySPExists(keySP.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Admin/KeySPs/Index");
            }
            return View(keySP);
        }

        // GET: Admin/KeySPs/Delete/5
        [HttpGet("Admin/KeySPs/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySP = await _context.KeySP
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keySP == null)
            {
                return NotFound();
            }

            return View(keySP);
        }

        // POST: Admin/KeySPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keySP = await _context.KeySP.FindAsync(id);
            _context.KeySP.Remove(keySP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeySPExists(int id)
        {
            return _context.KeySP.Any(e => e.Id == id);
        }
    }
}
