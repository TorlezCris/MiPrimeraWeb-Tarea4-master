using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Repositories.NorthWind;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomersRepository Customers { get; }
        IOrdersRepository Orders { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IUserRepository Users { get; }
        IProductsRepository Product { get; }
        ISupplierRepository Supplier { get; }
    }
}
