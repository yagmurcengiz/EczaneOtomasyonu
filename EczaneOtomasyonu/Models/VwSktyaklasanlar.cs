using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class VwSktyaklasanlar
{
    public string IlacAd { get; set; } = null!;

    public string? Barkod { get; set; }

    public DateOnly? SonKullanmaTarihi { get; set; }

    public int? Stok { get; set; }
}
