using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.NorthWind
{
    public interface IUserRepository
    {
        User ValidateUser(string email, string password);
        User CreateUser(User user);
    }
}
