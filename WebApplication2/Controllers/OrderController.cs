using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=db-mssql;Initial Catalog=s21522;Integrated Security=True";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "select * from [dbo].[Order]";
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

            sqlConnection.Dispose();

            return Ok(list);
        }
    }
}
