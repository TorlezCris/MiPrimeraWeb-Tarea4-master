using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class OrderHub : Hub
    {
        static List<string> orderIds = new List<string>();

        public void AddOrderId(string id)
        {
            if (!orderIds.Contains(id)) orderIds.Add(id);
            Clients.All.orderStatus(orderIds);
        }

        public void RemoveOrderId(string id)
        {
            if (orderIds.Contains(id)) orderIds.Remove(id);
            Clients.All.orderStatus(orderIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.orderStatus(orderIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}