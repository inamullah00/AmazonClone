using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Repository
{
    public interface IOrderRepository
    {

        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync([FromRoute] int id);
        Task<Order> AddOrderAsync([FromBody] Order order);
        Task<Order> UpdateOrderAsync([FromRoute] int id , [FromBody] Order order);
        Task<Order>DeleteOrderAsync([FromRoute] int id);

    }
}
