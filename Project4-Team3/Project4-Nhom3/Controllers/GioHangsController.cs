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
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using DomainLayer.DTO;
using System.Collections;
using Project4_Nhom3.Common;
using System.Security.Cryptography;

namespace Project4_Nhom3.Controllers
{
	public class GioHangsController : Controller
	{
		private readonly DataDbContext _context;
		private readonly IGioHangDTOService _gioHangDTOService;
		private readonly IGioHangService _gioHangService;
		private readonly ISanPhamService _sanPhamService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;

		public GioHangsController(DataDbContext context, IGioHangDTOService gioHangDTOService, ISanPhamService sanPhamService, IHttpContextAccessor httpContextAccessor, IGioHangService gioHangService)
		{
			_context = context;
			_gioHangDTOService = gioHangDTOService;
			_sanPhamService = sanPhamService;
			_httpContextAccessor = httpContextAccessor;
			_gioHangService = gioHangService;

		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var List = new List<GioHangDTO>();
			string cookie = HttpContext.Request.Headers["Cookie"].ToString();
			int start = cookie.IndexOf("; cart={") + 2;
			if (start>=2)
			{
				string cartJson = cookie.Substring(start + 5, cookie.IndexOf("}", start) - start - 4);
							var cart = JObject.Parse(cartJson);
							decimal? TongTien = 0;
							foreach (KeyValuePair<String, JToken> item in cart)
							{
								int id = int.Parse(item.Key);
								int SoLuong = int.Parse((string)item.Value);
								SanPham _sanPham = _sanPhamService.GetSanPham(id);
								List.Add(new GioHangDTO
								{
									SoLuong = SoLuong,
									TongTien = _sanPham.GiaTien * SoLuong,
									sanPham = _sanPham,
									user = _context.Users.FirstOrDefault()
								});
								TongTien += _sanPham.GiaTien * SoLuong;
							}
							ViewBag.TongTien = TongTien;
                return View(List);
            }
			else
			{
                return View(null);
            }
			
		}

		[HttpPost]
		public async Task<IActionResult> AddCart()
		{
			string cookie = HttpContext.Request.Headers["Cookie"].ToString();
			int start = cookie.IndexOf("; cart={") + 2;
			string cartJson = cookie.Substring(start + 5, cookie.IndexOf("}", start) - start - 4);
			var cart = JObject.Parse(cartJson);
			   
 			int userID = (int)_session.GetInt32("UserId");
			foreach (KeyValuePair<String, JToken> item in cart)
			{
				int id = int.Parse(item.Key);
				int SoLuong = int.Parse((string)item.Value);
				SanPham _sanPham = _sanPhamService.GetSanPham(id);
				GioHang gioHang = new GioHang
				{
					SanPhamId = id,
					UserId = userID,
					SoLuong = SoLuong,
					TongTien = _sanPham.GiaTien * SoLuong,
					NgayTao = DateTime.Now
				};
				_gioHangService.InsertGioHang(gioHang);
			}
			return Redirect("~/");
		}

		// GET: GioHangs/Details/5
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

		// GET: GioHangs/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: GioHangs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("SanPhamId,UserId,SoLuong,TongTien,NgayTao,Id")] GioHang gioHang)
		{
			if (ModelState.IsValid)
			{
				_context.Add(gioHang);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(gioHang);
		}

		// GET: GioHangs/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var gioHang = await _context.GioHang.FindAsync(id);
			if (gioHang == null)
			{
				return NotFound();
			}
			return View(gioHang);
		}

		// POST: GioHangs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("SanPhamId,UserId,SoLuong,TongTien,NgayTao,Id")] GioHang gioHang)
		{
			if (id != gioHang.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(gioHang);
					await _context.SaveChangesAsync();
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
				return RedirectToAction(nameof(Index));
			}
			return View(gioHang);
		}

		// GET: GioHangs/Delete/5
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

		// POST: GioHangs/Delete/5
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
