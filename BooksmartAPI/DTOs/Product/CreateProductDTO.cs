using BooksmartAPI.Models;

namespace BooksmartAPI.DTOs.Product
{
    public class CreateProductDTO
    {
        public string BarCodeValue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public List<string> Genres { get; set; } = new();
        public int Pages { get; set; }
        public float Price { get; set; }
        public float RentPrice { get; set; }
    }
}
