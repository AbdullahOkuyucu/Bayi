using AppCore.DataAccess.Bases.EntityFramework;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories
{
    public abstract class KullaniciRepoBase : RepositoryBase<Hesap>
    {
        public KullaniciRepoBase(DbContext db) : base(db)
        {
        }
    }
    public class KullaniciRepo : KullaniciRepoBase
    {
        public KullaniciRepo(DbContext db) : base(db)
        {
        }
    }
}
