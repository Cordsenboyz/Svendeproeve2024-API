using BooksmartAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories.Base
{
    public abstract class BaseRepository<TEntity, TId, TDbContext> where TEntity : BaseEntity<TId> where TDbContext : DbContext
    {

        protected readonly BsDbContext _context;

        public BaseRepository(BsDbContext context)
        {
            _context = context;
        }

        protected abstract DbSet<TEntity> Set { get; }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await Set.AddAsync(entity);

            return true;
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            bool invalidNumber = id is int numId && numId < 0;
            bool invalidString = id is string stringId && string.IsNullOrEmpty(stringId);

            if (invalidNumber || invalidString) throw new ArgumentNullException(nameof(id));

            TEntity? entity = await Set.FindAsync(id);
            if (entity is null) throw new EntityNotFoundException<TEntity, TId>(nameof(entity), entity);

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await Set.ToListAsync() ?? new List<TEntity>();

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            TEntity? existing = await GetAsync(entity.Id);
            if (existing is null) throw new EntityNotFoundException<TEntity, TId>(nameof(existing), existing);

            Set.Update(entity);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TId id)
        {
            TEntity? existing = await GetAsync(id);
            if (existing is null) throw new EntityNotFoundException<TEntity, TId>(nameof(existing), existing);

            return await DeleteAsync(existing);
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity is null) throw new EntityNotFoundException<TEntity, TId>(nameof(entity), entity);

            Set.Remove(entity);
            return true;

        }
    }
}
