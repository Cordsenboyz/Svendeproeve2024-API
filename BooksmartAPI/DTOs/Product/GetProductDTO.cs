using BooksmartAPI.Models;

namespace BooksmartAPI.DTOs.Product
{
    public class GetProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public List<string> Genres { get; set; } = new();
        public int Pages { get; set; }
        public string BarCode { get; set; }
        public int Count { get; set; }
    }
}
