using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class HesapModel : RecordBase
    {
        [Required]
        [MaxLength(20)]
        [DisplayName("Kullanıcı Adı ")]
        public string? KullaniciAdi { get; set; }

        [Required]
        [StringLength(8)]
        public string? Sifre { get; set; }

        public bool Aktif { get; set; }

        public int HesapDetayiId { get; set; }
        public HesapDetayModel? HesapDetayi { get; set; }

        public int RolId { get; set; }
        public RolModel? RolModel { get; set; }
    }
}
