using System.ComponentModel.DataAnnotations;

namespace EczaneOtomasyonu.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required]
        public string KullaniciAdi { get; set; }

        [Required]
        public string Sifre { get; set; }

        
        public string Rol { get; set; }
    }
}