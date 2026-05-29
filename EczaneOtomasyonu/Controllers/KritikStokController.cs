using Microsoft.AspNetCore.Mvc;
using EczaneOtomasyonu.Models; // Kendi model yolun
using Microsoft.EntityFrameworkCore;

public class KritikStokController : Controller
{
    private readonly EczaneOtomasyonuContext _context;

    public KritikStokController(EczaneOtomasyonuContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Raporun adını buraya yaz (VwKritikStokRaporu gibi)
        var rapor = await _context.VwKritikStokRaporus.ToListAsync();
        return View(rapor);
    }
}
