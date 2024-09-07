using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class CategoryRepository : BaseRepository<Category, Guid, BsDbContext>
    {
        public CategoryRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Category> Set => _context.Categories;

        public async Task<Category> GetCategoryByName(string name) => await Set.FirstOrDefaultAsync(category => category.Name == name);
    }
}
