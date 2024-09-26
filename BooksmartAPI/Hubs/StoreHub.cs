using BooksmartAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace BooksmartAPI.Hubs
{
    public class StoreHub : Hub
    {
        public async Task SendUpdate(Product product)
        {
            await Clients.All.SendAsync("Update", product);
        }

        public async Task Test(Product product)
        {
            await Clients.All.SendAsync("Test", product);
        }
    }
}
