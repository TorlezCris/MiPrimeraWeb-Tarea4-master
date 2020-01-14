using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface ICustomersRepository: IRepository<Customers>
    {
        Customers GetById(string id);
        bool Update(Customers customer);
        bool Delete(string id);
        IEnumerable<Customers> PagedList(int startRow, int endRow);
        int Count();
    }
}
