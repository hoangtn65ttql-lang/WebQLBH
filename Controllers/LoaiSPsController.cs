using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class LoaiSPsController : Controller
    {
        private readonly AppDbContext _context;

        public LoaiSPsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            => View(_context.LoaiSP_DS());

        public IActionResult Details(short id)
        {
            var obj = _context.LoaiSP_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenNSP = _context.NhomSP_DS()
                .FirstOrDefault(n => n.MaNSP == obj.MaNSP)?.TenNSP;
            return View(obj);
        }

        public IActionResult Create()
        {
            ViewBag.MaNSP = new SelectList(_context.NhomSP_DS(), "MaNSP", "TenNSP");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoaiSP obj)
        {
            _context.LoaiSP_Them(obj.TenLSP!, obj.MaNSP);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(short id)
        {
            var obj = _context.LoaiSP_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaNSP = new SelectList(_context.NhomSP_DS(), "MaNSP", "TenNSP", obj.MaNSP);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, LoaiSP obj)
        {
            _context.LoaiSP_Sua(obj.MaLSP, obj.TenLSP!, obj.MaNSP);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(short id)
        {
            var obj = _context.LoaiSP_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenNSP = _context.NhomSP_DS()
                .FirstOrDefault(n => n.MaNSP == obj.MaNSP)?.TenNSP;
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            _context.LoaiSP_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}