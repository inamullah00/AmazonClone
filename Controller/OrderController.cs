using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;
using EcommerceApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public readonly EcommerceDBContext _dbContext;
        public readonly IMapper _mapper;
        public readonly IOrderRepository _orderRepository;
        public readonly UserManager<ApplicationUser> _userManager;
        public OrderController(EcommerceDBContext context ,IMapper mapper , IOrderRepository orderRepository , UserManager<ApplicationUser> userManager)
        {
            _dbContext = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userManager = userManager;

        }

     //--->> Get All Orders

        [HttpGet]
        [Route("Orders")]

        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderRepository.GetOrdersAsync();

   
            return Ok(new
            {
                Data= order,
            });
        }

     //--->> Get Single Order

        [HttpGet]
        [Route("Order/{id}")]

        public async Task<IActionResult> GetSingleOrder(int id)
        {
            
           var order =  await _orderRepository.GetOrderAsync(id);
           
            return Ok(new 
            {Data= order
            });
            
           
        }




     //---->> Add Order

        [HttpPost]
        [Route("Order")]

        public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderdto)
        {

            if (orderdto == null)
            {
                return BadRequest();
            }
            // Get the CustomUser from the database using UserManager
            var user = await _userManager.FindByIdAsync(orderdto.UserId);

              if (user == null)
            {
                // Handle the case where the user with the given ID is not found
                return NotFound("User not found");
            }

               // Convert Data from DTO into Domin
            var orderData = _mapper.Map<Order>(orderdto);
            //    //DominOrder.ApplicationUser = user;

            var result =   await _orderRepository.AddOrderAsync(orderData);

            return Ok(new
            {
                Message = "Order Placed Successfully!",
                Data = result
            }) ;

        }


     //----->> Update Order

        [HttpPut]
        [Route("Order/{id}")]

        public async Task<IActionResult> UpdateOrder([FromRoute] int id , OrderDTO orderdto)
        {
            var dominOrder = _mapper.Map<Order>(orderdto);
            var  order = await _orderRepository.UpdateOrderAsync(id, dominOrder);

            var resultDto = _mapper.Map<OrderDTO>(order);
            
            return Ok(new
            {
                Message = "Order Updated Successfully!",
                Data = resultDto
            });

        }

     //--->> Delete Order


        [HttpDelete]
        [Route("Order/{id:Guid}")]

        public async  Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
           var resultOrder =  await _orderRepository.DeleteOrderAsync(id);

            var orderdto = _mapper.Map<OrderDTO>(resultOrder);
            return Ok(new
            {
                Message="Order Deleted Successfully!",
                Data= orderdto
            });
        }





      

    }
}
