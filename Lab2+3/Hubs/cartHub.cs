using Microsoft.AspNetCore.SignalR;

namespace Lab2_3.Hubs
{
    public class CartHub : Hub
    {
        public async Task ChangeQuantity(List<Object> list)
        {
            await Clients.All.SendAsync("ShowQuantity", list);
        }
    }
}
