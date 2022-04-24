using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class HesapGirisModel
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [MaxLength(15)]
        [DisplayName("Kullanıcı Adı")]
        public string? KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(9)]
        [DisplayName("Şifre")]
        public string? Sifre { get; set; }
    }
}
