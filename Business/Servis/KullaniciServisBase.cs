using AppCore.Business.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Data.Entity.Repositories;
using Entities.Entities;

namespace Business.Servis
{
    public interface IKullaniciServis : IService<HesapModel>
    {
    }

    public class KullaniciServis : IKullaniciServis
    {
        private readonly KullaniciRepoBase _kullaniciRepo;

        public KullaniciServis(KullaniciRepoBase kullaniciRepo)
        {
            _kullaniciRepo = kullaniciRepo;
        }
        public Result Add(HesapModel model)
        {
            try
            {
                if (_kullaniciRepo.EntityQuery().Any(u => u.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim()))
                    return new ErrorResult("Aynı kullanıcı adına sahip kayıt var!");
                if (_kullaniciRepo.EntityQuery("HesapDetayi").Any(u => u.HesapDetayi.EPosta.ToUpper() == model.HesapDetayi.EPosta.ToUpper().Trim()))
                    return new ErrorResult("Aynı e-postaya sahip kullanıcı var!");
                var entity = new Hesap()
                {
                    Aktif = model.Aktif,
                    KullaniciAdi = model.KullaniciAdi.Trim(),
                    Sifre = model.Sifre.Trim(),
                    RolId = model.RolId,
                    HesapDetayi = new HesapDetayi()
                    {
                        Adres = model.HesapDetayi.Adres.Trim(),
                        EPosta = model.HesapDetayi.EPosta.Trim()
                    }
                };
                _kullaniciRepo.Add(entity);
                return new SuccessResult("Yeni Kullanıcı Oluşturuldu.");
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _kullaniciRepo.Dispose();
        }

        public IQueryable<HesapModel> Query()
        {
            return _kullaniciRepo.EntityQuery("Rol", "HesapDetayi").Select(h => new HesapModel()
            {
                Aktif = h.Aktif,
                KullaniciAdi = h.KullaniciAdi,
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
