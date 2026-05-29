using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;
using Microsoft.AspNetCore.Authorization;

namespace EczaneOtomasyonu.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] // 👈 BU ODAYA SADECE ADMİNLER GİREBİLİR!
    [Area("Admin")]
    
    public class HastalarsController : Controller
    {
   
        private readonly EczaneOtomasyonuContext _context;

        public HastalarsController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // GET: Hastalars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hastalars.ToListAsync());
        }

        // GET: Hastalars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars
                .FirstOrDefaultAsync(m => m.HastaId == id);
            if (hastalar == null)
            {
                return NotFound();
            }

            return View(hastalar);
        }

        // GET: Hastalars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hastalars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HastaId,Tc,Ad,Soyad,Telefon,KayitTarihi")] Hastalar hastalar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastalar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hastalar);
        }

        // GET: Hastalars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars.FindAsync(id);
            if (hastalar == null)
            {
                return NotFound();
            }
            return View(hastalar);
        }

        // POST: Hastalars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HastaId,Tc,Ad,Soyad,Telefon,KayitTarihi")] Hastalar hastalar)
        {
            if (id != hastalar.HastaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastalarExists(hastalar.HastaId))
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
            return View(hastalar);
        }

        // GET: Hastalars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars
                .FirstOrDefaultAsync(m => m.HastaId == id);
            if (hastalar == null)
            {
                return NotFound();
            }

            return View(hastalar);
        }

        // POST: Hastalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hastalar = await _context.Hastalars.FindAsync(id);
            if (hastalar != null)
            {
                _context.Hastalars.Remove(hastalar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastalarExists(int id)
        {
            return _context.Hastalars.Any(e => e.HastaId == id);
        }
    }
}
