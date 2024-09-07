using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Product : BaseEntity<Guid>
    {
        public Barcode BarCode { get; set; }
    }
}
