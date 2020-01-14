using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        //Metodo adicional
        IEnumerable<OrderDetails> GetListByOrderId(int id);
        bool UpdateOrderDetailCustomized(OrderDetails orderDetails);
    }
}
