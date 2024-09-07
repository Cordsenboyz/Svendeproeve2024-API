using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class AuthorRepository : BaseRepository<Author, Guid, BsDbContext>
    {
        public AuthorRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Author> Set => _context.Authors;

        public async Task<Author> GetAuthorByName(string name) => await Set.FirstOrDefaultAsync(author => author.Name == name);
    }
}
