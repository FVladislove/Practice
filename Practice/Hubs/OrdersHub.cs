using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Practice.Hubs
{
    public class OrdersHub : Hub
    {
        public async Task AddOrder(string order)
        {
            await Clients.All.SendAsync("addOrder", order);
        }

        public async Task RemoveOrder(string orderId)
        {
            await Clients.All.SendAsync("removeOrder", orderId);
        }
    }
}
