using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class TinhsController : Controller
    {
        private readonly AppDbContext _context;

        public TinhsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tinhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tinh.ToListAsync());
        }

        // GET: Tinhs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh
                .FirstOrDefaultAsync(m => m.MaTinh == id);
            if (tinh == null)
            {
                return NotFound();
            }

            return View(tinh);
        }

        // GET: Tinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTinh,TenTinh")] Tinh tinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinh);
        }

        // GET: Tinhs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh.FindAsync(id);
            if (tinh == null)
            {
                return NotFound();
            }
            return View(tinh);
        }

        // POST: Tinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTinh,TenTinh")] Tinh tinh)
        {
            if (id != tinh.MaTinh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinhExists(tinh.MaTinh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tinh);
        }

        // GET: Tinhs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh
                .FirstOrDefaultAsync(m => m.MaTinh == id);
            if (tinh == null)
            {
                return NotFound();
            }

            return View(tinh);
        }

        // POST: Tinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tinh = await _context.Tinh.FindAsync(id);
            if (tinh != null)
            {
                _context.Tinh.Remove(tinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinhExists(string id)
        {
            return _context.Tinh.Any(e => e.MaTinh == id);
        }
    }
}
