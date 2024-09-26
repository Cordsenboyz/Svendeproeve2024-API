namespace BooksmartAPI.DTOs.Order
{
    public class CreateOrderDTO
    {
        public List<string> productBarCodes { get; set; }
        public bool Delivery { get; set; }
    }
}
