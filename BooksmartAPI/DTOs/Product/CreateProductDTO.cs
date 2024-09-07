using BooksmartAPI.Models;

namespace BooksmartAPI.DTOs.Product
{
    public class CreateProductDTO
    {
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public int Pages { get; set; }
    }
}
