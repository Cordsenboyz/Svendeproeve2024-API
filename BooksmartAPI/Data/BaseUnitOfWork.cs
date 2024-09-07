using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Data
{
    public abstract class BaseUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public BaseUnitOfWork(TDbContext context)
        {
            Context = context;
        }

        protected TDbContext Context { get; }

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();
    }
}
