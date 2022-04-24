using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class HesapDetayModel : RecordBase
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        public string? EPosta { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(500)]
        public string? Adres { get; set; }
    }
}
