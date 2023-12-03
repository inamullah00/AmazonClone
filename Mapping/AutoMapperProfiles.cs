using AutoMapper;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            // Mapper for Order
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

            //Mapper for products


            CreateMap<ProductsDTO, Products>().ReverseMap();

            CreateMap<Products ,ProductsDTO>().ReverseMap();


            // Mapper for Order Item

            CreateMap<OrderItemDTO, OrderItems>().ReverseMap();
            CreateMap<OrderItems, OrderItemDTO>().ReverseMap();


            // Mapper for Category

            CreateMap<AddCategoryDTO,Categories>().ReverseMap();

            CreateMap<GetCategoryDTO, Categories>().ReverseMap();

        }

    }
}
