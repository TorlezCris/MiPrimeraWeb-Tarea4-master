using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface IProductsRepository : IRepository<Products>
    {
        bool CreateCustomized(Products product);
        bool UpdateCustomized(Products product);
        Products GetByIdCustomized(int id);
        bool DeleteCustomized(int ProductID);
        IEnumerable<Products> PagedList(int startRow, int endRow);
        int Count();
    }
}
