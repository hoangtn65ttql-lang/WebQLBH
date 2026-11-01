using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class XaPhuongsController : Controller
    {
        private readonly AppDbContext _context;

        public XaPhuongsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            => View(_context.XaPhuong_DS());

        public IActionResult Details(string id)
        {
            var obj = _context.XaPhuong_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenTinh = _context.Tinh_DS()
                .FirstOrDefault(t => t.MaTinh == obj.MaTinh)?.TenTinh;
            return View(obj);
        }

        public IActionResult Create()
        {
            ViewBag.MaTinh = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(XaPhuong obj)
        {
            _context.XaPhuong_Them(obj.MaXP!, obj.TenXP!, obj.MaTinh!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            var obj = _context.XaPhuong_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaTinh = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh", obj.MaTinh);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, XaPhuong obj)
        {
            _context.XaPhuong_Sua(obj.MaXP!, obj.TenXP!, obj.MaTinh!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            var obj = _context.XaPhuong_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenTinh = _context.Tinh_DS()
                .FirstOrDefault(t => t.MaTinh == obj.MaTinh)?.TenTinh;
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _context.XaPhuong_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}