using BooksmartAPI.Repositories.Base;

namespace BooksmartAPI.Models
{
    public class Author : BaseEntity<Guid>
    {
        public string Name {  get; set; }
    }
}
