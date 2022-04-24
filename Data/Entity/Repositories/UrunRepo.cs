using Data.Entity.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories
{
    public class UrunRepo : UrunRepoBase
    {
        public UrunRepo(DbContext db) : base(db)
        {
        }
    }
}
