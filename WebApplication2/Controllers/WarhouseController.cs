using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication2.DataAccess;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarhouseController : Controller
    {
        private readonly IWarhousesDataAccess _orderDataAccess;

        public WarhouseController(IWarhousesDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(int amount)
        {        
           return Ok(await _orderDataAccess.GetOrdersAsync(amount));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] Warhouse warhouse)
        {
            return Ok(await _orderDataAccess.AddProductToWarehouseAsync(warhouse));
        }
    }
}
