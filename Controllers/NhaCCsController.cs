using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class NhaCCsController : Controller
    {
        private readonly AppDbContext _context;

        public NhaCCsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.DanhSachXP = _context.XaPhuong_DS();
            return View(_context.NhaCC_DS());
        }

        public IActionResult Details(short id)
        {
            var obj = _context.NhaCC_CT(id);
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
        public IActionResult Create(NhaCC obj)
        {
            _context.NhaCC_Them(obj.TenNCC!, obj.DienThoai, obj.Email, obj.DiaChi!, obj.MaXP!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(short id)
        {
            var obj = _context.NhaCC_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaXP = new SelectList(_context.XaPhuong_DS(), "MaXP", "TenXP", obj.MaXP);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, NhaCC obj)
        {
            _context.NhaCC_Sua(obj.MaNCC, obj.TenNCC!, obj.DienThoai, obj.Email,
                obj.DiaChi!, obj.MaXP!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(short id)
        {
            var obj = _context.NhaCC_CT(id);
            if (obj == null) return NotFound();
            ViewBag.TenXP = _context.XaPhuong_DS()
                .FirstOrDefault(x => x.MaXP == obj.MaXP)?.TenXP;
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            _context.NhaCC_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}