using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Barcode : BaseEntity<Guid>
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Author Author { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public int Pages { get; set; }
    }
}
