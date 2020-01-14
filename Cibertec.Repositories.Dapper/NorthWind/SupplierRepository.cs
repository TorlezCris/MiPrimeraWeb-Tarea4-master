using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    public class SupplierRepository : Repository<Suppliers>, ISupplierRepository
    {

        public SupplierRepository(string connectionString) : base(connectionString)
        {

        }


        public Suppliers GetByIdCustomized(int SupplierID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //return connection.Get<Products>(ProductID);
                return connection.GetAll<Suppliers>().Where(x => x.SupplierID == SupplierID).First();
                //return connection.Query<Customers>(x => x.CustomerID == id)
            }
        }

        public bool CreateCustomized(Suppliers supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("insert into [dbo].[Suppliers]([CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], [Phone], [Fax], [Country])" +
                                        " values(@CompanyName, " +
                                        "@ContactName, " +
                                        "@ContactTitle, " +
                                        "@Address, " +
                                        "@City, " +
                                        "@Region, " +
                                        "@PostalCode, " +
                                        "@Phone, " +
                                        "@Fax, " +
                                        "@Country)",
                                        new
                                        {
                                            supplier.CompanyName,
                                            supplier.ContactName,
                                            supplier.ContactTitle,
                                            supplier.Address,
                                            supplier.City,
                                            supplier.Region,
                                            supplier.PostalCode,
                                            supplier.Phone,
                                            supplier.Fax,
                                            supplier.Country,
                                        });

                return result > 0 ? true : false;
            }
        }



        public bool UpdateCustomized(Suppliers supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("update [Suppliers] " +
                                        "set ContactName = @ContactName, " +
                                        "CompanyName = @CompanyName, " +
                                        "ContactTitle = @ContactTitle, " +
                                        "Address = @Address, " +
                                        "City = @City, " +
                                        "Region = @Region, " +
                                        "PostalCode = @PostalCode, " +
                                        "Country = @Country, " +
                                        "Phone = @Phone, " +
                                        "Fax = @Fax " +
                                        "where SupplierID = @SupplierID",
                                        new
                                        {
                                            supplier.ContactName,
                                            supplier.CompanyName,
                                            supplier.ContactTitle,
                                            supplier.Address,
                                            supplier.City,
                                            supplier.Region,
                                            supplier.PostalCode,
                                            supplier.Country,
                                            supplier.Phone,
                                            supplier.Fax,
                                            supplier.SupplierID
                                        });

                return result > 0 ? true : false;
            }
        }


        //public bool DeleteCustomized(Customers customer)
        public bool DeleteCustomized(int SupplierID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute("delete " +
                                        "from Suppliers " +
                                        "where SupplierID = @SupplierID",
                                        new { SupplierID });

                return result > 0 ? true : false;
            }
        }

        public IEnumerable<Suppliers> PagedList(int startRow, int endRow)
        {
            if (startRow >= endRow) return new List<Suppliers>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<Suppliers>("dbo.uspSupplierPagedList", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(SupplierID) from dbo.Suppliers");
            }
        }
    }
}
