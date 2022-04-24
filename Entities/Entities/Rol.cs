using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Rol : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string? Adi { get; set; }

        public List<Hesap>? Hesap { get; set; }
    }
}
