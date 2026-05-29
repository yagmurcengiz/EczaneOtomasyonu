
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace EczaneOtomasyonu.Controllers
{
    public class GirisController : Controller
    {
        private readonly EczaneOtomasyonuContext _context;

        public GirisController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // 1. Giriş Sayfasını Açan Kısım (GET)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. Butona Basıldığında Bilgileri Kontrol Eden Kısım (POST)
        [HttpPost]
        public async Task<IActionResult> Index(string kullaniciAdi, string sifre)
        {
            // Veritabanında kullanıcı adı ve şifre eşleşiyor mu?
            var user = await _context.Kullanicilar
                .FirstOrDefaultAsync(u => u.KullaniciAdi == kullaniciAdi && u.Sifre == sifre);

            if (user != null)
            {
                // Kullanıcı doğruysa ona bir kimlik kartı (Claim) hazırlıyoruz
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.KullaniciAdi),
                    new Claim(ClaimTypes.Role, user.Rol) // Veritabanındaki "Admin" veya "Musteri" rolünü karta yazıyoruz
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                // Kimlik kartını tarayıcının çerezlerine (Cookie) güvenli bir şekilde basıyoruz
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                // Karttaki role göre kullanıcıyı doğru odaya fırlatıyoruz
                if (user.Rol == "Admin")
                {
                    return RedirectToAction("Index", "Ilaclars", new { area = "Admin" });
                }
                else if (user.Rol == "Musteri")
                {
                    return RedirectToAction("Index", "Urunler", new { area = "Musteri" });
                }
            }

            // Bilgiler yanlışsa sayfaya hata mesajı gönder
            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // 3. Güvenli Çıkış Yapma Metodu
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }
        // 1. Kayıt Ol Sayfasını Açan Kısım (GET)
        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        // 2. Form Gönderildiğinde Kullanıcıyı Veritabanına Kaydeden Kısım (POST)
        [HttpPost]
        public async Task<IActionResult> KayitOl(Kullanici yeniKullanici)
        {
            // Aynı kullanıcı adından veritabanında zaten var mı kontrolü
            var varMi = await _context.Kullanicilar
                .AnyAsync(u => u.KullaniciAdi == yeniKullanici.KullaniciAdi);

            if (varMi)
            {
                ViewBag.Hata = "Bu kullanıcı adı zaten alınmış!";
                return View(yeniKullanici);
            }

            // 💡 İŞTE ÇÖZÜM BURASI: C#'a "Formdan Rol gelmedi diye kızma, onu ben kodda vereceğim" diyoruz.
            ModelState.Remove("Rol");

            if (ModelState.IsValid)
            {
                // Dışarıdan kaydolan herkes otomatik "Musteri" olur
                yeniKullanici.Rol = "Musteri";

                _context.Kullanicilar.Add(yeniKullanici);
                await _context.SaveChangesAsync();

                // Kayıt başarılı olunca kullanıcıyı giriş ekranına yönlendiriyoruz
                return RedirectToAction("Index");
            }

            ViewBag.Hata = "Lütfen formdaki alanları eksiksiz doldurun.";
            return View(yeniKullanici);
        }
    }

        }
    

