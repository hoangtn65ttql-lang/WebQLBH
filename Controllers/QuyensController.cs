using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class QuyensController : Controller
    {
        private readonly AppDbContext _context;

        public QuyensController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            => View(_context.Quyen_DS());

        public IActionResult Details(byte id)
        {
            var obj = _context.Quyen_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Quyen obj)
        {
            _context.Quyen_Them(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(byte id)
        {
            var obj = _context.Quyen_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(byte id, Quyen obj)
        {
            _context.Quyen_Sua(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(byte id)
        {
            var obj = _context.Quyen_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.Quyen_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}