using BooksmartAPI.Models;

namespace BooksmartAPI.DTOs.Order
{
    public class GetOrderDTO
    {
        public List<OrderProduct> Products { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public bool Delivery { get; set; }
    }
}
