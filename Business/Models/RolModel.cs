using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RolModel : RecordBase
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(50)]
        [DisplayName("Rol")]
        public string? Adi { get; set; }

        public List<HesapModel> Hesap { get; set; }
    }
}
