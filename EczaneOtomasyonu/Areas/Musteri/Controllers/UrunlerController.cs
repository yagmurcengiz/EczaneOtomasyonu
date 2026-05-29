using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;
using Microsoft.AspNetCore.Authorization; // Modellerine erişmek için

namespace EczaneOtomasyonu.Areas.Musteri.Controllers
{
    [Authorize(Roles = "Musteri,Admin")] // Müşteri ekranını hem müşteri hem de eczacı inceleyebilsin diye iki rolü de yazdık
    [Area("Musteri")]
    public class UrunlerController : Controller
    
    
    {
        // Veritabanı bağlantısı
        private readonly EczaneOtomasyonuContext _context;

        public UrunlerController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // Giriş yapan müşteri sadece ilaç listesini okur
        // Müşteri İlaç Listesi ve Arama Motoru
        public async Task<IActionResult> Index(string aramaKelimesi)
        {
            // Arama kutusuna yazılan yazıyı sayfada hafızada tutmak için:
            ViewData["ArananKelime"] = aramaKelimesi;

            // Önce bütün ilaçları seçmeye hazırlanıyoruz
            var ilaclar = from i in _context.Ilaclars select i;

            // Eğer arama kutusu boş değilse (bir şey yazılmışsa) filtreleme yap
            if (!string.IsNullOrEmpty(aramaKelimesi))
            {
                // İçinde aranan harfler geçen ilaçları filtrele
                ilaclar = ilaclar.Where(i => i.IlacAdi.Contains(aramaKelimesi));
            }

            return View(await ilaclar.ToListAsync());
        }
    }
    }
