using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Repository
{
    public interface IProductsRepository
    {
        Task<List<Products>> GetAllAsync();
        Task<Products> GetSingleProductAsync([FromRoute] int id);
        Task<Products> AddProductAsync([FromBody] Products product);
        
        Task<Products> UpdateProductAync([FromRoute] int id,[FromBody] Products product);
        Task<Products> DeleteProductAsync([FromBody] int id);
    }
}
