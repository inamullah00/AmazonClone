using EcommerceApp.Models.Domin;

namespace EcommerceApp.Repository
{
    public interface IITemOrderRepository
    {

        Task<List<OrderItems>> GetOrderItemAsync();
        Task<List<OrderItems>> GetOrderItemAsync(int id);

        Task<OrderItems> DeleteOrderItemAsync(int id);

        Task<OrderItems> UpdateOrderItemAsync(int id , OrderItems item);

        Task<OrderItems> AddOrderItemAsync(OrderItems orderItems);


    }
}
