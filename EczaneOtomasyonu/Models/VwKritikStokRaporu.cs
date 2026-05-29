using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class VwKritikStokRaporu
{
    public string IlacAdi { get; set; } = null!;

    public string BarkodNo { get; set; } = null!;

    public string KategoriAdi { get; set; } = null!;

    public int StokMiktari { get; set; }

    public decimal BirimFiyat { get; set; }
}
