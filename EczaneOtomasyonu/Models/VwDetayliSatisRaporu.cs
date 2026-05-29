using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class VwDetayliSatisRaporu
{
    public int SatisId { get; set; }

    public DateTime SatisTarihi { get; set; }

    public string HastaBilgisi { get; set; } = null!;

    public string SatisYapanPersonel { get; set; } = null!;

    public decimal ToplamTutar { get; set; }
}
