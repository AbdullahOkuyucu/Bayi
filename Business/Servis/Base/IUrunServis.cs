using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Business.Models.Filtre;
using Business.Models.Rapor;

namespace Business.Servis.Base
{
    public interface IUrunServis : IService<UrunModel>
    {
        Result<List<UrunRaporuModel>> UrunRaporuGetir(UrunRaporuFiltreModel filtre = null, PageModel sayfa = null, OrderModel sira = null);
    }
}
