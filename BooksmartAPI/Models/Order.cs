using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Order : BaseEntity<Guid>
    {
        public List<OrderProduct> Products { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public bool Delivery { get; set; }
        public User User { get; set; }
    }
}
