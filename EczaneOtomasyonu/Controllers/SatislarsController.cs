using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;

namespace EczaneOtomasyonu.Controllers
{
    public class SatislarsController : Controller
    {
        private readonly EczaneOtomasyonuContext _context;

        public SatislarsController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // GET: Satislars
        public async Task<IActionResult> Index()
        {
            var eczaneOtomasyonuContext = _context.Satislars.Include(s => s.Hasta).Include(s => s.Personel);
            return View(await eczaneOtomasyonuContext.ToListAsync());
        }

        // GET: Satislars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislars
                .Include(s => s.Hasta)
                .Include(s => s.Personel)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satislar == null)
            {
                return NotFound();
            }

            return View(satislar);
        }

        // GET: Satislars/Create
        public IActionResult Create()
        {
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "HastaId");
            ViewData["PersonelId"] = new SelectList(_context.Personellers, "PersonelId", "PersonelId");
            return View();
        }

        // POST: Satislars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SatisId,SatisTarihi,HastaId,PersonelId,ToplamTutar")] Satislar satislar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satislar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "HastaId", satislar.HastaId);
            ViewData["PersonelId"] = new SelectList(_context.Personellers, "PersonelId", "PersonelId", satislar.PersonelId);
            return View(satislar);
        }

        // GET: Satislars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislars.FindAsync(id);
            if (satislar == null)
            {
                return NotFound();
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "HastaId", satislar.HastaId);
            ViewData["PersonelId"] = new SelectList(_context.Personellers, "PersonelId", "PersonelId", satislar.PersonelId);
            return View(satislar);
        }

        // POST: Satislars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SatisId,SatisTarihi,HastaId,PersonelId,ToplamTutar")] Satislar satislar)
        {
            if (id != satislar.SatisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satislar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatislarExists(satislar.SatisId))
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
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "HastaId", satislar.HastaId);
            ViewData["PersonelId"] = new SelectList(_context.Personellers, "PersonelId", "PersonelId", satislar.PersonelId);
            return View(satislar);
        }

        // GET: Satislars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislars
                .Include(s => s.Hasta)
                .Include(s => s.Personel)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satislar == null)
            {
                return NotFound();
            }

            return View(satislar);
        }

        // POST: Satislars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satislar = await _context.Satislars.FindAsync(id);
            if (satislar != null)
            {
                _context.Satislars.Remove(satislar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatislarExists(int id)
        {
            return _context.Satislars.Any(e => e.SatisId == id);
        }
    }
}
