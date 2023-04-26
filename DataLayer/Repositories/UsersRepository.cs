using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public User GetByEmail(string email)
        {
            return GetRecords().FirstOrDefault(u => u.Email == email);
        }
    }
}
