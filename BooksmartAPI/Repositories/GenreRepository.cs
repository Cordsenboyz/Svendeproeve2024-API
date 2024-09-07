using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class GenreRepository : BaseRepository<Genre, Guid, BsDbContext>
    {
        public GenreRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Genre> Set => _context.Genres;

        public async Task<Genre> GetByName(string name) => await Set.FirstOrDefaultAsync(genre => genre.Name == name);
    }
}
