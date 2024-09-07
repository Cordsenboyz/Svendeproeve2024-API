using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class BarcodeRepository : BaseRepository<Barcode, Guid, BsDbContext>
    {
        public BarcodeRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Barcode> Set => _context.Barcodes;

        public async Task<Barcode> GetBarcodeByBarcode(string barcodeInput) => await Set
            .Include(barcode => barcode.Author)
            .Include(barcode => barcode.Category)
            .Include(barcode => barcode.Genres)
            .FirstOrDefaultAsync(barcode => barcode.Value == barcodeInput);
    }
}
