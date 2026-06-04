using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EczaneOtomasyonu.Models;
using System.Threading.Tasks;

namespace EczaneOtomasyonu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CanliSepetlerController : Controller
    {
        private readonly EczaneOtomasyonuContext _context;

        public CanliSepetlerController(EczaneOtomasyonuContext context)
        {
            _context = context;
        }

        // Adminin tüm sepetleri gördüğü ekran (/Admin/CanliSepetler)
        public async Task<IActionResult> Index()
        {
            var tumSepetler = await _context.Sepet
                .Include(s => s.Hasta)
                .Include(s => s.Ilac)
                .OrderByDescending(s => s.EklemeTarihi)
                .ToListAsync();

            return View(tumSepetler);
        }
    }
}