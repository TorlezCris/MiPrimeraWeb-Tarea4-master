using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    public class OrdersRepository : Repository<oRDERS>, IOrdersRepository
    {
        public OrdersRepository(string connectionString) : base(connectionString)
        {
        }

        public oRDERS GetByIdCustomized(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<oRDERS>().Where(
                order => order.ORDERid.Equals(id)).First();
            }
        }

        public bool UpdateOrderCustomized(oRDERS order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("update Orders " +
                                            "set ShipName = @shipName, " +
                                            "ShippedDate = @shippedDate, " +
                                            "ShipVia = @shipVia, " +
                                            "where OrderID = @myId",
                                            new
                                            {
                                                shipName = order.ShipName,
                                                shippedDate = order.ShippedDate,
                                                shipVia = order.ShipVia,
                                                myId = order.ORDERid
                                            });
                /*
                if (result > 0) return true;
                else return false;
                */
                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<oRDERS> PagedList(int startRow, int endRow)
        {
            if (startRow >= endRow) return new List<oRDERS>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<oRDERS>("dbo.uspOrderPagedList", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(ORDERid) from dbo.Orders");
            }
        }
    }
}
