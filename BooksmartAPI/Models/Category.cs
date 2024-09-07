using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
