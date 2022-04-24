using AppCore.Business.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Data.Entity.Repositories;

namespace Business.Servis
{
    public interface IHesapDetayServis : IService<HesapModel>
    {
        Result<List<HesapModel>> KullanicilariListele();
        Result<HesapModel> KullaniciListele(int id);
    }

    public class HesapDetayServis : IHesapDetayServis
    {
        private readonly KullaniciRepoBase _kullaniciRepoBase;

        public HesapDetayServis(KullaniciRepoBase kullaniciRepoBase)
        {
            _kullaniciRepoBase = kullaniciRepoBase;
        }


        public Result Add(HesapModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            try
            {
                _kullaniciRepoBase.DeleteEntity(id);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Dispose()
        {
            _kullaniciRepoBase.Dispose();
        }

        public Result<List<HesapModel>> KullanicilariListele()
        {
            try
            {
                var kullanicilar = Query().ToList();
                if (kullanicilar == null || kullanicilar.Count == 0)
                    return new ErrorResult<List<HesapModel>>("Kullanıcı Bulunumadı!");
                return new SuccessResult<List<HesapModel>>(kullanicilar);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Result<HesapModel> KullaniciListele(int id)
        {
            try
            {
                var kullanici = Query().SingleOrDefault(u => u.Id == id);
                if (kullanici == null)
                    return new ErrorResult<HesapModel>("Kullanıcı Bulunumadı!");
                return new SuccessResult<HesapModel>(kullanici);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public IQueryable<HesapModel> Query()
        {
            return _kullaniciRepoBase.EntityQuery("Rol", "HesapDetayi").Select(h => new HesapModel()
            {
                Aktif = h.Aktif,
                KullaniciAdi = h.KullaniciAdi,
                Guid = h.Guid,
                Id = h.Id,
                Sifre = h.Sifre,
                RolId = h.RolId,
                HesapDetayiId = h.HesapDetayiId,

                RolModel = new RolModel()
                {
                    Adi = h.Rol.Adi
                },
                HesapDetayi = new HesapDetayModel()
                {
                    EPosta = h.HesapDetayi.EPosta,
                    Adres = h.HesapDetayi.Adres
                }
            });
        }
        public Result Update(HesapModel model)
        {
            throw new NotImplementedException();
        }
    }
}
