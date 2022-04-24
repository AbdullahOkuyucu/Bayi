using AppCore.Business.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Data.Entity.Repositories;
using Entities.Entities;

namespace Business.Servis
{
    public interface IRolServis : IService<RolModel>
    {

    }
    
    public class RolServis : IRolServis
    {
        private readonly RolRepoBase _rolRepo;

        public RolServis(RolRepoBase rolRepo)
        {
            _rolRepo = rolRepo;
        }
        public Result Add(RolModel model)
        {
            try
            {
                if (_rolRepo.Query().Any(r => r.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                    return new ErrorResult("Aynı isimde rol kaydı var!");
                var entity = new Rol()
                {
                    Adi = model.Adi.Trim()
                };
                _rolRepo.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var role = _rolRepo.EntityQuery(r => r.Id == id, "Hesap").SingleOrDefault();
                if (role == null)
                    return new ErrorResult("Rol bulunamadı!");
                if (role.Hesap != null && role.Hesap.Count > 0)
                    return new ErrorResult("Kullanıcılar olduğu için rol silinemez!");
                _rolRepo.Delete(role);
                return new SuccessResult("Rol başarıyla silindi.");
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Dispose()
        {
            _rolRepo.Dispose();
        }

        public IQueryable<RolModel> Query()
        {
            return _rolRepo.EntityQuery("Hesap")
                .OrderBy(r => r.Adi)
                .Select(r => new RolModel()
                {
                    Id = r.Id,
                    Guid = r.Guid,
                    Adi = r.Adi,
                    Hesap = r.Hesap.Select(u => new HesapModel()
                    {
                        Id = u.Id,
                        Aktif = u.Aktif,
                        KullaniciAdi = u.KullaniciAdi
                    }).ToList()
                });
        }

        public Result Update(RolModel model)
        {
            try
            {
                if (_rolRepo.Query().Any(r => r.Adi.ToUpper() == model.Adi.ToUpper().Trim() && r.Id != model.Id))
                    return new ErrorResult("Aynı isimde rol var!");
                var entity = new Rol()
                {
                    Id = model.Id,
                    Adi = model.Adi.Trim()
                };
                _rolRepo.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}