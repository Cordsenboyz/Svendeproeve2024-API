using BooksmartAPI.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

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
        public float Price { get; set; }
        public float RentPrice { get; set; }
    }
}
