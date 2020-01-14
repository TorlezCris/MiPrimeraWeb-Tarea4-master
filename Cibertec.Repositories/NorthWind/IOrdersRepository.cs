using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface IOrdersRepository : IRepository<oRDERS>
    {
        oRDERS GetByIdCustomized(int id);
        bool UpdateOrderCustomized(oRDERS order);
        IEnumerable<oRDERS> PagedList(int startRow, int endRow);
        int Count();
    }
}
