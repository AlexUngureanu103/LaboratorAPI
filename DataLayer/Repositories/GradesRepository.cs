using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class GradesRepository : RepositoryBase<Grade>
    {
        private readonly AppDbContext dbContext;

        public GradesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


    }
}
