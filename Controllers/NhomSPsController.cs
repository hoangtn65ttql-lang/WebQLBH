using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class NhomSPsController : Controller
    {
        private readonly AppDbContext _context;

        public NhomSPsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Danh sách
        public IActionResult Index()
        {
            return View(_context.NhomSP_DS());
        }

        // GET: Chi tiết
        public IActionResult Details(byte id)
        {
            var obj = _context.NhomSP_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // GET: Form thêm mới
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lưu thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NhomSP obj)
        {
            _context.NhomSP_Them(obj.TenNSP!);
            return RedirectToAction(nameof(Index));
        }

        // GET: Form sửa
        public IActionResult Edit(byte id)
        {
            var obj = _context.NhomSP_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // POST: Lưu sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(byte id, NhomSP obj)
        {
            _context.NhomSP_Sua(obj.MaNSP, obj.TenNSP!);
            return RedirectToAction(nameof(Index));
        }

        // GET: Xác nhận xóa
        public IActionResult Delete(byte id)
        {
            var obj = _context.NhomSP_CT(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // POST: Thực hiện xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(byte id)
        {
            _context.NhomSP_Xoa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}