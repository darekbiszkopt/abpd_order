using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DataAccess
{
    public interface IWarhousesDataAccess
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int amount);
        Task<Order> AddProductToWarehouseAsync(Warhouse warhouse);
    }
}
