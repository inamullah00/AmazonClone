using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository
{
    public class OrderRepository : IOrderRepository
    {

        public readonly EcommerceDBContext _dbContext;
        public OrderRepository(EcommerceDBContext context) 
        { 
        
            _dbContext = context;
        }
      
        //------- Get All
        public async Task<List<Order>> GetOrdersAsync()
        {
            var order = await _dbContext.orders.Include("ApplicationUser").ToListAsync();


            if (order == null)
            {
                return null;
            }

            return order;
        }
        //------- Get Single
        public async Task<Order> GetOrderAsync([FromRoute] int id)
        {


       var ExistingOrder = await _dbContext.orders.Include("ApplicationUser").FirstOrDefaultAsync(o => o.UserId == id.ToString());


            if (ExistingOrder == null)
            {
                return null;
            }

            return ExistingOrder;
        }

     //------>> Add
        public async  Task<Order> AddOrderAsync([FromBody] Order order)
        {
            if(order == null)
            {
                return null;
            }
              await _dbContext.orders.AddAsync(order);
            _dbContext.SaveChanges();

            return order;

             
        }

    //----->>  Update
        public async Task<Order> UpdateOrderAsync([FromRoute] int id, [FromBody] Order order)
        {

            if (id == null)
            {
                return null;
            }

           var existingOrder = await _dbContext.orders.FirstOrDefaultAsync(order=>order.Id == id);

            if(existingOrder == null)
            {
                return null;
            }

       
            existingOrder.Address = order.Address;
            existingOrder.TotalAmount = order.TotalAmount; 
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.UserId = order.UserId;

            _dbContext.SaveChanges();
            return existingOrder;


        }

    //----->>  Delete
        public async  Task<Order> DeleteOrderAsync([FromRoute] int id)
        {
            if(id == null)
            {
                return null;
            }

            var existingOrder = await _dbContext.orders.FirstOrDefaultAsync(order=>order.Id==id);

            if(existingOrder == null)
            {
                return null;
            }
            
            _dbContext.orders.Remove(existingOrder);

            _dbContext.SaveChanges();

            return existingOrder;
        }

    }
}
