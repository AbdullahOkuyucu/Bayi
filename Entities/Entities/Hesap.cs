using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Hesap : RecordBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string? KullaniciAdi { get; set; }

        [Required]
        [StringLength(12)]
        public string? Sifre { get; set; }

        public bool Aktif { get; set; }

        public int HesapDetayiId { get; set; }
        public HesapDetayi? HesapDetayi { get; set; }

        public int RolId { get; set; }
        public Rol? Rol { get; set; }
    }
}
