using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository
{
    public class ItemOrderRepository : IITemOrderRepository
    {

        public readonly EcommerceDBContext dbContext;
        public ItemOrderRepository(EcommerceDBContext context)
        {
              dbContext = context;
        }

    //---> Add
        public async Task<OrderItems> AddOrderItemAsync(OrderItems orderItems)
        {
            if(orderItems == null)
            {
                return null;
            }
            await dbContext.OrderItems.AddAsync(orderItems);
            await dbContext.SaveChangesAsync();
            return orderItems;
        }
    //---> Delete
        public async Task<OrderItems> DeleteOrderItemAsync(int id)
        {

            var OrderItem = await dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == id);

           if(OrderItem == null)
            {
                return null;
            }

            dbContext.OrderItems.Remove(OrderItem);
            await dbContext.SaveChangesAsync();

            return OrderItem;
        }

    //---> GetAll
        public async Task<List<OrderItems>> GetOrderItemAsync()
        {

            return await dbContext.OrderItems.Include(o => o.Order).ThenInclude(d=>d.OrderItems).Include(p => p.Products).ToListAsync();
            
        }
    //---> Get Single
        public Task<List<OrderItems>> GetOrderItemAsync(int id)
        {
            throw new NotImplementedException();
        }
    //---> Update
        public Task<OrderItems> UpdateOrderItemAsync(int id, OrderItems item)
        {
            throw new NotImplementedException();
        }
    }
}
