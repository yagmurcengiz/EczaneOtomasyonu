using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EczaneOtomasyonu.Controllers
{
    public class SepetController : Controller
    {
        private readonly EczaneOtomasyonuContext _context;

        public SepetController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // Hastanın kendi sepetini gördüğü yer (Örn: /Sepet/Index)
        public async Task<IActionResult> Index()
        {
            // Şimdilik sisteme giriş yapan sahte bir hasta ID'si alıyoruz (Örn: HastaID = 1)
            // Normalde burası sistemdeki aktif kullanıcıdan (Session/Auth) gelir.
            int aktifHastaId = 1;

            var sepetim = await _context.Sepet
                .Include(s => s.Ilac)
                .Where(s => s.HastaId == aktifHastaId)
                .ToListAsync();

            return View(sepetim);
        }

        // İlaç listesinden tıklanınca çalışacak buton (Örn: /Sepet/Ekle/5)
        public async Task<IActionResult> Ekle(int id)
        {
            int aktifHastaId = 1; // Sahte aktif hasta

            // İlaç zaten sepette var mı kontrol et
            var mevcutSepet = await _context.Sepet
                .FirstOrDefaultAsync(s => s.HastaId == aktifHastaId && s.IlacId == id);

            if (mevcutSepet != null)
            {
                mevcutSepet.Adet += 1; // Varsa adet artır
                _context.Update(mevcutSepet);
            }
            else
            {
                // Yoksa yeni sepet satırı aç
                var yeniSepet = new Sepet
                {
                    HastaId = aktifHastaId,
                    IlacId = id,
                    Adet = 1,
                    EklemeTarihi = System.DateTime.Now
                };
                _context.Add(yeniSepet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }   // Sepetim sayfasına fırlat
            // Sepetten ilaç silme komutu
            // Adet azaltma ve gerekirse sepetten tamamen silme komutu
        public async Task<IActionResult> Sil(int id)
        {
            // Silinmek istenen sepet satırını bul
            var sepetOgesi = await _context.Sepet.FindAsync(id);

            if (sepetOgesi != null)
            {
                // Eğer sepetteki ilaç sayısı 1'den büyükse sadece adetini 1 azalt
                if (sepetOgesi.Adet > 1)
                {
                    sepetOgesi.Adet -= 1;
                    _context.Update(sepetOgesi); // Güncelleme komutu
                }
                else
                {
                    // Eğer zaten 1 taneyse, artık sepetten tamamen uçur
                    _context.Sepet.Remove(sepetOgesi);
                }

                await _context.SaveChangesAsync(); // Değişiklikleri SQL'e işle
            }

            return RedirectToAction("Index"); // Sepetim sayfasına geri fırlat
        }
    }
    }
