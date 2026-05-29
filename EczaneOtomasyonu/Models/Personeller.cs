using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Personeller
{
    public int PersonelId { get; set; }

    public string Tc { get; set; } = null!;

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string? Unvan { get; set; }

    public virtual ICollection<Satislar> Satislars { get; set; } = new List<Satislar>();
}
