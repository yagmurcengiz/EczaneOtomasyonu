using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Hastalar
{
    public int HastaId { get; set; }

    public string Tc { get; set; } = null!;

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string? Telefon { get; set; }

    public DateTime? KayitTarihi { get; set; }

    public virtual ICollection<Satislar> Satislars { get; set; } = new List<Satislar>();
}
