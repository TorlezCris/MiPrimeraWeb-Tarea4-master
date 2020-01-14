using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public User ValidateUser(string email, string password)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", password);

                return connection.QueryFirstOrDefault<User>("dbo.uspValidateUser",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public User CreateUser(User user)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", user.Email);
                parameters.Add("@password", user.Password);
                parameters.Add("@firstName", user.FirstName);
                parameters.Add("@lastName", user.LastName);

                return connection.QueryFirstOrDefault<User>("dbo.uspCreateUser",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
