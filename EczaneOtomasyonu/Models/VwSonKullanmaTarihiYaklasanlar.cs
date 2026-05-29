using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class VwSonKullanmaTarihiYaklasanlar
{
    public string IlacAdi { get; set; } = null!;

    public string BarkodNo { get; set; } = null!;

    public DateOnly? SonKullanmaTarihi { get; set; }

    public int StokMiktari { get; set; }
}
