using BooksmartAPI.Data;
using BooksmartAPI.Models;
using BooksmartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksmartAPI.Repositories
{
    public class OrderRepository : BaseRepository<Order, Guid, BsDbContext>
    {
        public OrderRepository(BsDbContext context) : base(context) { }

        protected override DbSet<Order> Set => _context.Orders;

        public async Task<IEnumerable<Order>> GetOrdersByEmail(string email) => await Set
            .Include(order => order.Products)
            .Where(order => order.User.Email == email)
            .ToListAsync();
    }
}
