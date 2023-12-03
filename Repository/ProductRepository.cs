using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository
{
    public class ProductRepository : IProductsRepository

    {

        public readonly EcommerceDBContext _dbContext;
        public ProductRepository(EcommerceDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //........ Get All
        public async Task<List<Products>> GetAllAsync()
        {
            //ThenInclude just work with 0=>0. etc 
            //return await _dbContext.products.Include("ApplicationUser").ToListAsync();// Include ApplicationUser within the Order


            return await _dbContext.products.ToListAsync();
        }

        //........Get  Single 
        public async  Task<Products> GetSingleProductAsync([FromRoute] int id)
        {
            if (id == null)
            {
                return null;
            }

       

        var product = await _dbContext.products .Include("Categories")
         
             .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return null;
            }
            return product;
        }





        //........ Add
        public async  Task<Products> AddProductAsync([FromBody] Products product)
        {
            if (product == null)
            {
                return null;
            }

            await _dbContext.products.AddAsync(product);
            _dbContext.SaveChanges();
            return product;
        }


        //........ Update
        public async Task<Products> UpdateProductAync([FromRoute] int id, [FromBody] Products product)
        {

            if (id == null && product == null)
            {
                return null;

            }

            var isExistProduct = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == id);
            if (isExistProduct == null)
            {
                return null;
            }

            isExistProduct.Name = product.Name;
            isExistProduct.Description = product.Description;
            isExistProduct.Price = product.Price;
            isExistProduct.Brand = product.Brand;
            isExistProduct.Size = product.Size;
     

            _dbContext.SaveChanges();

            return isExistProduct;
        }


        //........ Delete
        public async Task<Products> DeleteProductAsync([FromRoute] int id)
        {

            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == id); 

            if(product == null)
            {
                return null;
            }

             _dbContext.products.Remove(product); 
            await _dbContext.SaveChangesAsync();
            return   product;
        }



    }
}
