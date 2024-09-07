using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class OrderProduct : BaseEntity<Guid>
    {
        public Barcode BarCode { get; set; }
    }
}
