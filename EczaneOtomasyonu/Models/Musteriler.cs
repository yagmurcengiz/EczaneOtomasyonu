using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Musteriler
{
    public int MusteriId { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string? Telefon { get; set; }
}
