using AppCore.DataAccess.Bases.EntityFramework;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories
{
    public abstract class HesapDetayRepoBase : RepositoryBase<HesapDetayi>
    {
        protected HesapDetayRepoBase(DbContext db) : base(db)
        {
        }
    }

    public class HesapDetayiBase : HesapDetayRepoBase
    {
        public HesapDetayiBase(DbContext db) : base(db)
        {

        }
    }
}
