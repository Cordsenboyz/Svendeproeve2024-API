using BooksmartAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Data
{
    public class UnitOfWork : BaseUnitOfWork<BsDbContext>
    {
        public UnitOfWork(BsDbContext context) : base(context)
        {
            Products = new(context);
            Barcodes = new(context);
            Genres = new(context);
            Categories = new(context);
            Authors = new(context);
            Orders = new(context);
        }

        public ProductRepository Products { get; set; }
        public BarcodeRepository Barcodes { get; set; }
        public GenreRepository Genres { get; set; }
        public CategoryRepository Categories { get; set; }
        public AuthorRepository Authors { get; set; }
        public OrderRepository Orders { get; set; }
    }
}
