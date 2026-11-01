using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SanPhamsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
            => View(_context.SanPham_DS());

        public IActionResult Details(string id)
        {
            var obj = _context.SanPham_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenLSP = _context.LoaiSP_DS()
                .FirstOrDefault(l => l.MaLSP == obj.MaLSP)?.TenLSP;
            ViewBag.TenDVT = _context.DonViTinh_DS()
                .FirstOrDefault(d => d.MaDVT == obj.MaDVT)?.TenDVT;
            return View(obj);
        }

        public IActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(_context.LoaiSP_DS(), "MaLSP", "TenLSP");
            ViewBag.MaDVT = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham obj, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString("N")
                               + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await imageFile.CopyToAsync(fs);
                obj.AnhSP = fileName;
            }
            _context.SanPham_Them(obj.TenSP!, obj.DonGia,
                obj.AnhSP, obj.MoTaCT, obj.MaLSP, obj.MaDVT);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            var obj = _context.SanPham_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaLSP = new SelectList(_context.LoaiSP_DS(), "MaLSP", "TenLSP", obj.MaLSP);
            ViewBag.MaDVT = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT", obj.MaDVT);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SanPham obj,
            IFormFile? imageFile, string? existingAnh)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString("N")
                               + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await imageFile.CopyToAsync(fs);
                if (!string.IsNullOrEmpty(existingAnh))
                {
                    var old = Path.Combine(uploads, existingAnh);
                    if (System.IO.File.Exists(old)) System.IO.File.Delete(old);
                }
                obj.AnhSP = fileName;
            }
            else
            {
                obj.AnhSP = existingAnh;
            }
            _context.SanPham_Sua(obj.MaSP!, obj.TenSP!, obj.DonGia,
                obj.AnhSP, obj.MoTaCT, obj.MaLSP, obj.MaDVT);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            var obj = _context.SanPham_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenLSP = _context.LoaiSP_DS()
                .FirstOrDefault(l => l.MaLSP == obj.MaLSP)?.TenLSP;
            ViewBag.TenDVT = _context.DonViTinh_DS()
                .FirstOrDefault(d => d.MaDVT == obj.MaDVT)?.TenDVT;
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _context.SanPham_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}