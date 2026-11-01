using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class PhongBansController : Controller
    {
        private readonly AppDbContext _context;

        public PhongBansController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            => View(_context.PhongBan_DS());

        public IActionResult Details(byte id)
        {
            var obj = _context.PhongBan_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhongBan obj)
        {
            _context.PhongBan_Them(obj.TenPB!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(byte id)
        {
            var obj = _context.PhongBan_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(byte id, PhongBan obj)
        {
            _context.PhongBan_Sua(obj.MaPB, obj.TenPB!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(byte id)
        {
            var obj = _context.PhongBan_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.PhongBan_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}