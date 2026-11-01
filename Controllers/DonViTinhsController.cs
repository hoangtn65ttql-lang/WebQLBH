using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class DonViTinhsController : Controller
    {
        private readonly AppDbContext _context;

        public DonViTinhsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            => View(_context.DonViTinh_DS());

        public IActionResult Details(byte id)
        {
            var obj = _context.DonViTinh_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DonViTinh obj)
        {
            _context.DonViTinh_Them(obj.TenDVT!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(byte id)
        {
            var obj = _context.DonViTinh_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(byte id, DonViTinh obj)
        {
            _context.DonViTinh_Sua(obj.MaDVT, obj.TenDVT!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(byte id)
        {
            var obj = _context.DonViTinh_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.DonViTinh_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}