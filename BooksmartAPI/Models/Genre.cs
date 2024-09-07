using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Genre : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<Barcode> Barcodes { get; set; } = new();
    }
}
