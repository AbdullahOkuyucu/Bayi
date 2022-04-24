using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UrunModel : RecordBase
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Ürün Adı")]
        public string? Adi { get; set; }

        [DisplayName("Açıklaması")]
        public string? Aciklamasi { get; set; }

        [DisplayName("Birim Fiyatı")]
        public double BirimFiyati { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Stok Miktarı")]
        public int StokMiktari { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Kategori")]
        public int KategoriId { get; set; }
        
        //İhtiyaca göre sonradan eklenen özellikler.
        [DisplayName("Birim Fiyatı")]
        public string? BirimFiyatiModel { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public string? SonKullanmaTarihiModel { get; set; }
        public KategoriModel? KategoriModel { get; set; }

    }
}
