using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using Business.Models.Filtre;
using Business.Models.Rapor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bayi.Models
{
    public class UrunRaporuAjaxIndexViewModel
    {
        public List<UrunRaporuModel>? Urunler { get; set; }
        public UrunRaporuFiltreModel? Filtre { get; set; }
        public PageModel? Sayfa { get; set; }
        public SelectList? Sayfalar { get; set; }
        public OrderModel? Sira { get; set; }
    }
}
