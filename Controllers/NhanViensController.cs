using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NhanViensController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
            => View(_context.NhanVien_DS());

        public IActionResult Details(byte id)
            => View(_context.NhanVien_CT(id));

        public IActionResult Create()
        {
            // PhongBan_DS() là tên hàm trong AppDbContext mới
            ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nhanVien, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);
                nhanVien.AnhNV = fileName;
            }
            _context.NhanVien_Them(nhanVien);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(byte id)
        {
            var nhanVien = _context.NhanVien_CT(id);
            if (nhanVien == null) return NotFound();
            ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NhanVien nhanVien, IFormFile? image, string? existingAnh)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
            Directory.CreateDirectory(uploads);
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);
                if (!string.IsNullOrEmpty(existingAnh))
                {
                    var old = Path.Combine(uploads, existingAnh);
                    if (System.IO.File.Exists(old)) System.IO.File.Delete(old);
                }
                nhanVien.AnhNV = fileName;
            }
            else
            {
                nhanVien.AnhNV = existingAnh;
            }
            _context.NhanVien_Sua(nhanVien);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(byte id)
            => View(_context.NhanVien_CT(id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.NhanVien_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}