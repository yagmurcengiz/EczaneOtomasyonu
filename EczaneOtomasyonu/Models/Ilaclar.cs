using System;
using System.Collections.Generic;

namespace EczaneOtomasyonu.Models;

public partial class Ilaclar
{
    public int IlacId { get; set; }

    public string BarkodNo { get; set; } = null!;

    public string IlacAdi { get; set; } = null!;

    public int KategoriId { get; set; }

    public decimal BirimFiyat { get; set; }

    public int StokMiktari { get; set; }

    public DateOnly? SonKullanmaTarihi { get; set; }

    public virtual Kategoriler Kategori { get; set; } = null!;

    public virtual ICollection<SatisDetay> SatisDetays { get; set; } = new List<SatisDetay>();
}
