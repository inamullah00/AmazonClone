using AutoMapper;
using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;
using EcommerceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controller
{
    [EnableCors("AllowReactApp")]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {

        public EcommerceDBContext _dbContext;
        public readonly IMapper _mapper;
        public readonly IProductsRepository _productsRepository;

        public ProductController(EcommerceDBContext context , IMapper mapper ,IProductsRepository repository )
        {
            _dbContext = context;
            _mapper = mapper;
            _productsRepository = repository;
        }

// GetAll Products
        [HttpGet]
        [Route("Products")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllProducts()
        {
            var DominData = await _productsRepository.GetAllAsync();

            //var DTOData = _mapper.Map<List<ProductsDTO>>(DominData);

            return Ok(new
            {
                Data = DominData
            });
        }

// Get Single Product
        [HttpGet]
        [Route("Product/{id}")]

        public async Task<IActionResult> GetSingleProduct( int id)
        {
             var product = await _productsRepository.GetSingleProductAsync(id);
            return Ok(new
            {
                Data = product
            }); ;
        }

  // Add Product

 [HttpPost]
 [Route("AddProduct")]

 public async Task<IActionResult> AddProduct([FromBody] ProductsDTO DTOData)
    {
           
            var dominData = _mapper.Map<Products>(DTOData);

            var addedProduct = await _productsRepository.AddProductAsync(dominData);

            var dtoProduct =  _mapper.Map<ProductsDTO>(addedProduct);

            return Ok(new{

                        Message="Product Added Successfully!",
                        Data= dtoProduct
            });
    }


        // Update Product

        [HttpPut]
        [Route("Product/{id:Guid}")]

        public async Task<IActionResult> UpdateProduct([FromRoute] int id  , ProductsDTO DTOData)
        {
            var dominData =  _mapper.Map<Products>(DTOData);

          var updatedProduct  =  await  _productsRepository.UpdateProductAync(id, dominData);

            return Ok(new
            {
                Message = "Product Updated Successfully!",
                Data = updatedProduct
            }); 
        }

        // Delete Product

        [HttpDelete]
        [Route("Product/{id:Guid}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productsRepository.DeleteProductAsync(id);
            return Ok(new
            {
                Message = "Product Deleted Successfully",
                Data = product
            });
        }

    }
}
