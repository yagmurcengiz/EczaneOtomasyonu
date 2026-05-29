using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Satislar
{
    public int SatisId { get; set; }

    public DateTime SatisTarihi { get; set; }

    public int HastaId { get; set; }

    public int PersonelId { get; set; }

    public decimal ToplamTutar { get; set; }

    public virtual Hastalar Hasta { get; set; } = null!;

    public virtual Personeller Personel { get; set; } = null!;

    public virtual ICollection<SatisDetay> SatisDetays { get; set; } = new List<SatisDetay>();
}
