using System.ComponentModel;

namespace Business.Models
{
    public class SepetToplamiModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }

        [DisplayName("Ürün")]
        public string? UrunAdi { get; set; }

        [DisplayName("Fiyat")]
        public double BirimFiyatToplami { get; set; }

        [DisplayName("Adet")]
        public int ToplamUrunSayisi { get; set; }
    }
}
