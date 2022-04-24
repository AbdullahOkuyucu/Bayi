using AppCore.DataAccess.Bases.EntityFramework;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories.Base
{
    public abstract class KategoriRepoBase : RepositoryBase<Kategori>
    {
        protected KategoriRepoBase(DbContext db) : base(db)
        {
        }
    }

    public class KategoriRepository : KategoriRepoBase
    {
        public KategoriRepository(DbContext db) : base(db)
        {
        }
    }
}
