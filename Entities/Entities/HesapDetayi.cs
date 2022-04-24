using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class HesapDetayi : RecordBase
    {
        [Required]
        [StringLength(500)]
        public string? Adres { get; set; }

        [Required]
        [StringLength(100)]
        public string? EPosta { get; set; }
        public Hesap? Hesap { get; set; }

    }
}
