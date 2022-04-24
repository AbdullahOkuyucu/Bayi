using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class HesapKayitModel : RecordBase
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [MinLength(2, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [MinLength(2, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [MinLength(2, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Şifre Onay")]
        [Compare("Sifre", ErrorMessage = "{1} ve {0} ikinci şifre aynı olmalıdır!")]
        public string SifreTekrar { get; set; }

        public HesapDetayModel HesapDetayi { get; set; }
    }
}
