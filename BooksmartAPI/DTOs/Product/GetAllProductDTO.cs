namespace BooksmartAPI.DTOs.Product
{
    public class GetAllProductDTO
    {
        public string Name { get; set; }
        public BarcodeDTO Barcode { get; set; }
    }

    public class BarcodeDTO
    {
        public string Name { get; set; }
        public List<string> genre { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}
