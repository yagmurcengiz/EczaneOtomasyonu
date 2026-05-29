using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Kategoriler
{
    public int KategoriId { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public virtual ICollection<Ilaclar> Ilaclars { get; set; } = new List<Ilaclar>();
}
