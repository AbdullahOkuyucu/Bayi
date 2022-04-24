using AppCore.Business.Results;
using Business.Enum;
using Business.Models;

namespace Business.Servis
{
    public interface IHesapServis
    {
        Result Kayit(HesapKayitModel model);
        Result<HesapModel> GirisIslemi(HesapGirisModel model);
    }

    public class HesapServis : IHesapServis
    {
        private readonly IKullaniciServis _kullaniciServis;

        public HesapServis(IKullaniciServis kullaniciServis)
        {
            _kullaniciServis = kullaniciServis;
        }


        public Result<HesapModel> GirisIslemi(HesapGirisModel hesapGiris)
        {
            var hesap = _kullaniciServis.Query().SingleOrDefault(h => h.KullaniciAdi == hesapGiris.KullaniciAdi && h.Sifre == hesapGiris.Sifre && h.Aktif);
            if (hesap != null)
                return new SuccessResult<HesapModel>(hesap);
            return new ErrorResult<HesapModel>("Hesap Bulunamadı");
        }

        public Result Kayit(HesapKayitModel model)
        {
            try
            {
                var user = new HesapModel()
                {
                    Aktif = true,
                    KullaniciAdi = model.KullaniciAdi.Trim(),
                    Sifre = model.Sifre.Trim(),
                    RolId = (int)Roles.Kullanici,
                    HesapDetayi = new HesapDetayModel()
                    {
                        EPosta = model.HesapDetayi.EPosta.Trim(),
                        Adres = model.HesapDetayi.Adres.Trim()
                    }
                };
                return _kullaniciServis.Add(user);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
    }
}
