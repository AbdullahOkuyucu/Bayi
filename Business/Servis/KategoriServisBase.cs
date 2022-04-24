using AppCore.Business.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Data.Entity.Repositories.Base;

namespace Business.Servis
{
    public interface IKategoriServis : IService<KategoriModel>
    {
    }
    public class KategoriServis : IKategoriServis
    {
        private readonly KategoriRepoBase _kategoriRepo;

        public KategoriServis(KategoriRepoBase kategoriRepo)
        {
            _kategoriRepo = kategoriRepo;
        }

        public Result Add(KategoriModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _kategoriRepo.Dispose();
        }

        public IQueryable<KategoriModel> Query()
        {
            return _kategoriRepo.EntityQuery().OrderBy(h => h.Adi).Select(h => new KategoriModel()
            {
                Id = h.Id,
                Adi = h.Adi
            });
        }

        public Result Update(KategoriModel model)
        {
            throw new NotImplementedException();
        }
    }
}
