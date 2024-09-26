using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class ProductRepository : BaseRepository<Product, Guid, BsDbContext>
    {
        public ProductRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Product> Set => _context.Products;

        public async Task<Product> GetWithAllRelations(string barcode) => await Set
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Genres)
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Author)
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Category)
            .FirstOrDefaultAsync(product => product.BarCode.Value == barcode);

        public async Task<IEnumerable<Product>> GetAllWithAllRelations() => await Set
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Genres)
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Author)
            .Include(product => product.BarCode).ThenInclude(barcode => barcode.Category)
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetAllByBarcode(string barcode) => await Set
            .Where(product => product.BarCode.Value == barcode)
            .ToListAsync();
    }
}
