using AppCore.DataAccess.Bases.EntityFramework;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories.Base
{
    public abstract class UrunRepoBase : RepositoryBase<Urun>
    {
        protected UrunRepoBase(DbContext db) : base(db)
        {
        }
    }
}
