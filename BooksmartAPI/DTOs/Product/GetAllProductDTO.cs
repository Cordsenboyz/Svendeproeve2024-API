using Microsoft.Owin.Security;

namespace BooksmartAPI.DTOs.Product
{
    public class GetAllProductDTO
    {
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Barcode { get; set; }
    }
}
