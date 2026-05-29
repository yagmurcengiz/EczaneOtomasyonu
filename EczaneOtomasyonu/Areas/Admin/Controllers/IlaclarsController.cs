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
    public class IlaclarsController : Controller
    {
    
    
   
        private readonly EczaneOtomasyonuContext _context;

        public IlaclarsController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // GET: Ilaclars
        public async Task<IActionResult> Index()
        {
            var eczaneOtomasyonuContext = _context.Ilaclars.Include(i => i.Kategori);
            return View(await eczaneOtomasyonuContext.ToListAsync());
        }

        // GET: Ilaclars/Details/5
        public async Task<IActionResult> Details(string id) // int? olan yeri string yap
        {
            if (id == null) return NotFound();

            var ilaclar = await _context.Ilaclars
                .Include(i => i.Kategori)
                .FirstOrDefaultAsync(m => m.BarkodNo == id); // Hata burada sönecek

            if (ilaclar == null) return NotFound();

            return View(ilaclar);
        }
        // GET: Ilaclars/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategorilers, "KategoriId", "KategoriId");
            return View();
        }

        // POST: Ilaclars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IlacId,BarkodNo,IlacAdi,KategoriId,BirimFiyat,StokMiktari,SonKullanmaTarihi")] Ilaclar ilaclar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilaclar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategorilers, "KategoriId", "KategoriId", ilaclar.KategoriId);
            return View(ilaclar);
        }

        // GET: Ilaclars/Edit/5


        // GET: Ilaclars/Details/869001


        public async Task<IActionResult> Edit(string id) // int? olan yeri string yap
        {
            if (id == null) return NotFound();

            var ilaclar = await _context.Ilaclars
                .FirstOrDefaultAsync(m => m.BarkodNo == id); // Hata burada sönecek

            if (ilaclar == null) return NotFound();

            ViewData["KategoriId"] = new SelectList(_context.Kategorilers, "KategoriId", "KategoriAdi", ilaclar.KategoriId);
            return View(ilaclar);
        }

        // POST: Ilaclars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IlacId,BarkodNo,IlacAdi,KategoriId,BirimFiyat,StokMiktari,SonKullanmaTarihi")] Ilaclar ilaclar)
        {
            if (id != ilaclar.IlacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilaclar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlaclarExists(ilaclar.IlacId))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategorilers, "KategoriId", "KategoriId", ilaclar.KategoriId);
            return View(ilaclar);
        }

        // GET: Ilaclars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilaclar = await _context.Ilaclars
                .Include(i => i.Kategori)
                .FirstOrDefaultAsync(m => m.IlacId == id);
            if (ilaclar == null)
            {
                return NotFound();
            }

            return View(ilaclar);
        }

        // POST: Ilaclars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilaclar = await _context.Ilaclars.FindAsync(id);
            if (ilaclar != null)
            {
                _context.Ilaclars.Remove(ilaclar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlaclarExists(int id)
        {
            return _context.Ilaclars.Any(e => e.IlacId == id);
        }
    }
}
