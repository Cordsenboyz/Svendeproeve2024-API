using Microsoft.AspNetCore.Identity;

namespace BooksmartAPI.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
        public List<Rent> Rents { get; set; } = new();
    }
}
