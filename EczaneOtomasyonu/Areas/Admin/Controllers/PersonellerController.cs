using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;

namespace EczaneOtomasyonu.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PersonellerController : Controller
    {
        private readonly EczaneOtomasyonuContext _context;

        public PersonellerController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PersonelEkle()
        {
            return View();
        }

        // 💡 KESİN ÇÖZÜM: Verileri nesne olarak değil, doğrudan isim ve şifre olarak alıyoruz
        [HttpPost]
        public async Task<IActionResult> PersonelEkle(string kullaniciAdi, string sifre)
        {
            // Kutular boş mu kontrolü
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                ViewBag.Hata = "Lütfen alanları eksiksiz doldurun.";
                return View();
            }

            // Kullanıcı adı veritabanında var mı kontrolü
            var varMi = await _context.Kullanicilar
                .AnyAsync(u => u.KullaniciAdi == kullaniciAdi);

            if (varMi)
            {
                ViewBag.Hata = "Bu kullanıcı adı zaten sistemde kayıtlı!";
                return View();
            }

            // Her şey doğruysa yeni Admin nesnesini burada elimizle oluşturuyoruz
            Kullanici yeniPersonel = new Kullanici
            {
                KullaniciAdi = kullaniciAdi,
                Sifre = sifre,
                Rol = "Admin"
            };

            _context.Kullanicilar.Add(yeniPersonel);
            await _context.SaveChangesAsync();

            ViewBag.Basari = "Yeni eczacı/personel başarıyla sisteme eklendi!";
            return View();
        }
    }
}