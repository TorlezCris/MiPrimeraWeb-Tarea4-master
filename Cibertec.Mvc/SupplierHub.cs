using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cibertec.Mvc
{
    public class SupplierHub : Hub
    {
        static List<string> supplierIds = new List<string>();

        public void AddSupplierId(string id)
        {
            if (!supplierIds.Contains(id)) supplierIds.Add(id);
            Clients.All.supplierStatus(supplierIds);
        }

        public void RemoveSupplierId(string id)
        {
            if (supplierIds.Contains(id)) supplierIds.Remove(id);
            Clients.All.supplierStatus(supplierIds);
        }

        public override Task OnConnected()
        {
            return Clients.All.supplierStatus(supplierIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}