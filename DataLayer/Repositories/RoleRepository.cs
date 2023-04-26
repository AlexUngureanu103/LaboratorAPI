using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(AppDbContext context) : base(context) { }
    }
}
