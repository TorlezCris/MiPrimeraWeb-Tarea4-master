using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class ProductHub : Hub
    {
        static List<string> productIds = new List<string>();

        public void AddProductId(string id)
        {
            if (!productIds.Contains(id)) productIds.Add(id);
            Clients.All.productStatus(productIds);
        }

        public void RemoveProductId(string id)
        {
            if (productIds.Contains(id)) productIds.Remove(id);
            Clients.All.productStatus(productIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.productStatus(productIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}