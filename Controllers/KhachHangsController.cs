using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly AppDbContext _context;

        public KhachHangsController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            var ds = _context.KhachHang_DS();
            return View(ds);
        }

        public IActionResult Details(short id)
        {
            var obj = _context.KhachHang_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenXP = _context.XaPhuong_DS()
                .FirstOrDefault(x => x.MaXP == obj.MaXP)?.TenXP;
            return View(obj);
        }

        public IActionResult Create()
        {
            ViewBag.MaXP = new SelectList(_context.XaPhuong_DS(), "MaXP", "TenXP");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KhachHang obj)
        {
            _context.KhachHang_Them(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(short id)
        {
            var obj = _context.KhachHang_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaXP = new SelectList(_context.XaPhuong_DS(), "MaXP", "TenXP", obj.MaXP);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, KhachHang obj)
        {
            _context.KhachHang_Sua(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(short id)
        {
            var obj = _context.KhachHang_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            _context.KhachHang_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}