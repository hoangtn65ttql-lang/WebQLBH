using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class PhanQuyensController : Controller
    {
        private readonly AppDbContext _context;

        public PhanQuyensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PhanQuyens
        public IActionResult Index()
        {
            return View(_context.PhanQuyen_DS());
        }

        // GET: PhanQuyens/Details/5
        public IActionResult Details(byte id)
        {
            var obj = _context.PhanQuyen_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // GET: PhanQuyens/Create
        public IActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(_context.Quyen_DS(), "MaQuyen", "TenQuyen");
            ViewBag.MaNV = new SelectList(
                _context.NhanVien_DS().Select(n => new {
                    n.MaNV,
                    HoTen = n.HoNV + " " + n.TenNV
                }), "MaNV", "HoTen");
            return View();
        }

        // POST: PhanQuyens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhanQuyen obj)
        {
            _context.PhanQuyen_Them(obj);
            return RedirectToAction(nameof(Index));
        }

        // GET: PhanQuyens/Edit/5
        public IActionResult Edit(byte id)
        {
            var obj = _context.PhanQuyen_CT(id);
            if (obj == null) return NotFound();
            ViewBag.MaQuyen = new SelectList(_context.Quyen_DS(), "MaQuyen", "TenQuyen", obj.MaQuyen);
            ViewBag.MaNV = new SelectList(
                _context.NhanVien_DS().Select(n => new {
                    n.MaNV,
                    HoTen = n.HoNV + " " + n.TenNV
                }), "MaNV", "HoTen", obj.MaNV);
            return View(obj);
        }

        // POST: PhanQuyens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(byte id, PhanQuyen obj)
        {
            _context.PhanQuyen_Sua(obj);
            return RedirectToAction(nameof(Index));
        }

        // GET: PhanQuyens/Delete/5
        public IActionResult Delete(byte id)
        {
            var obj = _context.PhanQuyen_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // POST: PhanQuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.PhanQuyen_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}