using System;

namespace EczaneOtomasyonu.Models
{
    public class Sepet
    {
        public int SepetId { get; set; }
        public int HastaId { get; set; }
        public int IlacId { get; set; }
        public int Adet { get; set; }
        public DateTime EklemeTarihi { get; set; }

        // İlişkisel bağlar için Navigation Property
        public virtual Hastalar Hasta { get; set; }
        public virtual Ilaclar Ilac { get; set; }
    }
}