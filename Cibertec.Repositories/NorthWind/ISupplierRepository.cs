using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface ISupplierRepository : IRepository<Suppliers>
    {
        bool CreateCustomized(Suppliers product);
        bool UpdateCustomized(Suppliers product);
        Suppliers GetByIdCustomized(int id);
        bool DeleteCustomized(int ProductID);
        IEnumerable<Suppliers> PagedList(int startRow, int endRow);
        int Count();
    }
}
