using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Contrib;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    public class ProductRepository : Repository<Products>, IProductsRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {

        }

        public Products GetByIdCustomized(int ProductID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //return connection.Get<Products>(ProductID);
                return connection.GetAll<Products>().Where(x => x.ProductID == ProductID).First();
                //return connection.Query<Customers>(x => x.CustomerID == id)
            }
        }

        public bool CreateCustomized(Products product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("insert into [Products](ProductName, SupplierID, UnitPrice, UnitsInStock, Discontinued)" +
                                        " values(@ProductName, " +
                                        "@SupplierId, " +
                                        "@UnitPrice, " +
                                        "@UnitsInStock, " +
                                        "@Discontinued)",
                                        new
                                        {
                                            product.ProductName,
                                            product.SupplierId,
                                            product.UnitPrice,
                                            product.UnitsInStock,
                                            product.Discontinued
                                        });

                return result > 0 ? true : false;
            }
        }



        public bool UpdateCustomized(Products product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("update [Products] " +
                                        "set UnitPrice = @UnitPrice, " +
                                        "ProductName = @ProductName, " +
                                        "SupplierId = @SupplierId, " +
                                        "UnitsInStock = @UnitsInStock, " +
                                        "Discontinued = @Discontinued " +
                                        "where ProductID = @ProductID",
                                        new
                                        {
                                            product.UnitPrice,
                                            product.ProductName,
                                            product.SupplierId,
                                            product.UnitsInStock,
                                            product.Discontinued,
                                            product.ProductID
                                        });

                return result > 0 ? true : false;
            }
        }


        //public bool DeleteCustomized(Customers customer)
        public bool DeleteCustomized(int ProductID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("delete " +
                                        "from Products " +
                                        "where ProductID = @ProductID",
                                        new { ProductID });

                return result > 0 ? true : false;
            }
        }

        public IEnumerable<Products> PagedList(int startRow, int endRow)
        {
            if (startRow >= endRow) return new List<Products>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<Products>("dbo.uspProductPagedList", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(ProductID) from dbo.Products");
            }
        }
    }
}
