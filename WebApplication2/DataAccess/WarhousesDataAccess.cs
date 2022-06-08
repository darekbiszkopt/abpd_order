using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication2.DataAccess
{
    public class WarhousesDataAccess : IWarhousesDataAccess
    {
        private readonly IConfiguration _configuration;

        public WarhousesDataAccess(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<Order> AddProductToWarehouseAsync(Warhouse warhouse)
        {
            using SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultCon");


            SqlCommand sqlCommand = sqlConnection.CreateCommand();


            if (warhouse == null)
            {
                sqlCommand.CommandText = "select * from [dbo].[Order]";
            }
            else
            {
                sqlCommand.CommandText = "SELECT TOP 1 * FROM [dbo].[Order]" +
                    "WHERE [Order].IdProduct = @IdProduct";
                sqlCommand.Parameters.AddWithValue("@IdProduct", warhouse.IdProduct);
            }
            sqlCommand.Connection = sqlConnection;

            await sqlConnection.OpenAsync();

            SqlDataReader sdr = await sqlCommand.ExecuteReaderAsync();


            Order order = new Order();
            while (sdr.Read())
            {
                var orderX = new Order
                {
                    IdOrder = (int)sdr["idOrder"],
                    IdProduct = (int)sdr["idProduct"],
                    Amount = (int)sdr["Amount"],
                    CreatedAt = sdr["CreatedAt"].ToString()
                };
                order = orderX;
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int amount)
        {
            using SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _configuration.GetConnectionString("DefaultCon");


            SqlCommand sqlCommand = sqlConnection.CreateCommand();


            if (amount == 0)
            {
                sqlCommand.CommandText = "select * from [dbo].[Order]";

            }
            else
            {
                sqlCommand.CommandText = "select * from [dbo].[Order] where amount=@amount";
                sqlCommand.Parameters.AddWithValue("@amount", amount);
            }
            sqlCommand.Connection = sqlConnection;

            await sqlConnection.OpenAsync();

            SqlDataReader sdr = await sqlCommand.ExecuteReaderAsync();

            var list = new List<Order>();
            while (sdr.Read())
            {
                var order = new Order
                {
                    IdOrder = (int)sdr["idOrder"],
                    IdProduct = (int)sdr["idProduct"],
                    Amount = (int)sdr["Amount"],
                    CreatedAt = sdr["CreatedAt"].ToString()
                };
                list.Add(order);
            }

            return list;
        }
    }
}
