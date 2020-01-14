using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(string connectionString) : base(connectionString)
        {

        }

        //Metodos adicionales
        public IEnumerable<OrderDetails> GetListByOrderId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                /*1.LINQ*/
                //return connection.GetAll<OrderDetails>().Where(
                //    orderDetails => orderDetails.OrderId.Equals(id));

                /*2.QUERY con Dapper*/
                /*
                return connection.Query<OrderDetails>("Select OrderID,ProductID," +
                    "UnitPrice,Quantity,Discount " +
                    "from OrderDetails " +
                    "where OrderID = @myId", new { id }).ToList();
                */

                /*Lo que no se debe hacer: utilizar "select * from": */
                /*
                return connection.Query<OrderDetails>("Select * " +
                    "from OrderDetails " +
                    "where OrderID = @id", new { id }).ToList();
                */

                /*3.USP con Dapper*/
                /*Opcion 1*/
                /*
                return connection.Query<OrderDetails>("usp_GetDetailsByOrder",
                    new { OrderId = id }, commandType: CommandType.StoredProcedure);
                    */
                /*Opcion 2*/
                /*
                return connection.Query<OrderDetails>("exec usp_GetDetailsByOrder " +
                    "@id", new { id });
                    */
                /*3.USP con Dapper (Query y DynamicParameters)*/
                /*
                var queryParametrs = new DynamicParameters();
                queryParametrs.Add("@OrderId", id);
                return connection.Query<OrderDetails>("usp_GetDetailsByOrder",
                    queryParametrs, commandType: CommandType.StoredProcedure);
                */


                /*4. ADO.NET usando QueryString*/
                /*
                string query = "Select OrderID,ProductID," +
                    "UnitPrice,Quantity,Discount " +
                    "from OrderDetails " +
                    "where OrderID = @myId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@myId", id);
                List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.OrderId = Int32.Parse(reader["OrderId"].ToString());
                        orderDetails.ProductId = Int32.Parse(reader["ProductId"].ToString());
                        orderDetails.UnitPrice = Decimal.Parse(reader["UnitPrice"].ToString());
                        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                        orderDetails.Discount = Decimal.Parse(reader["Discount"].ToString());
                        lstOrderDetails.Add(orderDetails);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lstOrderDetails;
                */
                /*5.ADO.NET Opcion 2*/
                /*
                string query = "Select OrderID,ProductID," +
                    "UnitPrice,Quantity,Discount " +
                    "from OrderDetails " +
                    "where OrderID = {0}";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandText = string.Format(query, id);
                List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.OrderId = Int32.Parse(reader["OrderId"].ToString());
                        orderDetails.ProductId = Int32.Parse(reader["ProductId"].ToString());
                        orderDetails.UnitPrice = Decimal.Parse(reader["UnitPrice"].ToString());
                        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                        orderDetails.Discount = Decimal.Parse(reader["Discount"].ToString());
                        lstOrderDetails.Add(orderDetails);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lstOrderDetails;
                */

                /*Opcion 6: ADO.NET con USP*/

                SqlCommand command = new SqlCommand("usp_GetDetailsByOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", id);
                List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.OrderId = Int32.Parse(reader["OrderId"].ToString());
                        orderDetails.ProductId = Int32.Parse(reader["ProductId"].ToString());
                        orderDetails.UnitPrice = Decimal.Parse(reader["UnitPrice"].ToString());
                        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                        orderDetails.Discount = Decimal.Parse(reader["Discount"].ToString());
                        lstOrderDetails.Add(orderDetails);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lstOrderDetails;
            }
        }

        public bool UpdateOrderDetailCustomized(OrderDetails orderDetails)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var result = connection.Execute("update OrderDetails " +
                                            "set Quantity = @quantity, " +
                                            "UnitPrice = @unitPrice, " +
                                            "Discount = @discount, " +
                                            "where OrderID = @myId",
                                            new
                                            {
                                                quantity = orderDetails.Quantity,
                                                unitPrice = orderDetails.UnitPrice,
                                                discount = orderDetails.Discount,
                                                myId = orderDetails.OrderId
                                            });
                /*
                if (result > 0) return true;
                else return false;
                */
                return Convert.ToBoolean(result);
            }
        }
    }
}
