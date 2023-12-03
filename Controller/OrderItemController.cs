using AutoMapper;
using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;
using EcommerceApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        public readonly EcommerceDBContext dbContext;
        public readonly IMapper _mapper;
        public readonly IITemOrderRepository ItemOrderRepository;
        public OrderItemController(EcommerceDBContext context , IMapper mapper , IITemOrderRepository itemOrderRepository)
        {
            dbContext = context;
            _mapper = mapper;
            ItemOrderRepository = itemOrderRepository;
        }




        // --->> Get All

        [HttpGet]
        [Route("OrderItem")]
        public async Task<ActionResult> OrderItem()
        {

           var result =  await ItemOrderRepository.GetOrderItemAsync();

          //var resultDTO = _mapper.Map<OrderItemDTO>(result);
            return Ok(new
            {
                Data = result
            });

        }


        //---->> Get Single

        [HttpGet]
        [Route("orderItem/{id}")]
        public async Task<ActionResult> GetSingleOrderItem([FromRoute] int id)
        {
            var result = await ItemOrderRepository.GetOrderItemAsync(id);
            return null;

        }


        //---->> Add 


        [HttpPost]
        [Route("orderItem")]
        public async Task<ActionResult> AddOrderItem([FromBody] OrderItemDTO orderItemdto)
        {
            // Convert DTO into Domin
            var orderItem =  _mapper.Map<OrderItems>(orderItemdto);


            var result = await ItemOrderRepository.AddOrderItemAsync(orderItem);

            // convert Domin into DTO 

            var resultDTO = _mapper.Map<OrderItemDTO>(result);
            
            return Ok(new{

                Data = resultDTO

            });

        }

        //---->>Delete

        [HttpDelete]
        [Route("orderItem/{id}")]
        public async  Task<ActionResult> DeleteOrderItem([FromRoute] int id)
        {
            var result = await ItemOrderRepository.DeleteOrderItemAsync(id);
            return Ok(new
            {
                Message = "Item Deleted Successfully!",
                Data = result
            });

        }


        //---->> Update

        [HttpPut]
        [Route("orderItem/{id}")]
        public async Task<ActionResult> UpdateOrderItem([FromRoute] int id , [FromBody] OrderItems orderItems)
        {
            var result = ItemOrderRepository.UpdateOrderItemAsync(id, orderItems);
            return null;

        }




     


    }
}
