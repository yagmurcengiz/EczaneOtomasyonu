using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class SatisDetay
{
    public int SatisDetayId { get; set; }

    public int SatisId { get; set; }

    public int IlacId { get; set; }

    public int Miktar { get; set; }

    public decimal BirimFiyat { get; set; }

    public virtual Ilaclar Ilac { get; set; } = null!;

    public virtual Satislar Satis { get; set; } = null!;
}
