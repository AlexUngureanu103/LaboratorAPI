using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class RoleRepository : RepositoryBase<AvailableRole>
    {
        public RoleRepository(AppDbContext context) : base(context) { }
    }
}
