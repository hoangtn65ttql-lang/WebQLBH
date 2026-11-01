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
    public class DonMHsController : Controller
    {
        private readonly AppDbContext _context;

        public DonMHsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DonMHs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonMH.ToListAsync());
        }

        // GET: DonMHs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donMH = await _context.DonMH
                .FirstOrDefaultAsync(m => m.MaDMH == id);
            if (donMH == null)
            {
                return NotFound();
            }

            return View(donMH);
        }

        // GET: DonMHs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonMHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDMH,NgayDat,NgayGiao,MaNCC")] DonMH donMH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donMH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donMH);
        }

        // GET: DonMHs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donMH = await _context.DonMH.FindAsync(id);
            if (donMH == null)
            {
                return NotFound();
            }
            return View(donMH);
        }

        // POST: DonMHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDMH,NgayDat,NgayGiao,MaNCC")] DonMH donMH)
        {
            if (id != donMH.MaDMH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donMH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonMHExists(donMH.MaDMH))
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
            return View(donMH);
        }

        // GET: DonMHs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donMH = await _context.DonMH
                .FirstOrDefaultAsync(m => m.MaDMH == id);
            if (donMH == null)
            {
                return NotFound();
            }

            return View(donMH);
        }

        // POST: DonMHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var donMH = await _context.DonMH.FindAsync(id);
            if (donMH != null)
            {
                _context.DonMH.Remove(donMH);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonMHExists(string id)
        {
            return _context.DonMH.Any(e => e.MaDMH == id);
        }
    }
}
