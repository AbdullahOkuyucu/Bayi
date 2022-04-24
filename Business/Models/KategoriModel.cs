using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        [Required]
        [StringLength(50)]
        [DisplayName("Kategori Adı")]
        public string? Adi { get; set; }

        [StringLength(200)]
        public string? Aciklamasi { get; set; }
    }
}
