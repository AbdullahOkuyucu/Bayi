using AppCore.DataAccess.Bases.EntityFramework;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Repositories
{
    public abstract class RolRepoBase : RepositoryBase<Rol>
    {
        protected RolRepoBase(DbContext db) : base(db)
        {
        }
    }
    public class RolRepo : RolRepoBase
    {
        public RolRepo(DbContext db) : base(db)
        {
        }
    }
}
