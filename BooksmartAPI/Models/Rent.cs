using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Rent : BaseEntity<Guid>
    {
        public DateTime NextWeeklyPayment { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderProduct> Products { get; set; } = new();
        public User User { get; set; }
    }
}
