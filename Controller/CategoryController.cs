using AutoMapper;
using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        public  EcommerceDBContext _dbContext;
        public readonly IMapper _mapper;
        public CategoryController(EcommerceDBContext context , IMapper mapper )
        {
                   _dbContext = context;
            _mapper = mapper;
        }

        // --- Get All Category
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var ModelData = await _dbContext.Categories.Include("Products").ToListAsync();

            //Convert ModelData into DTO 
            //var DTOResult = _mapper.Map<GetCategoryDTO>(ModelData);
            return Ok(ModelData);
        }



        // --- Get Single Category
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingleCategory(int id)
        { 
            if(id== 0 || id == null)
            {
                return BadRequest();
            }

            var isProductExist  = await _dbContext.Categories.Include("Products").FirstOrDefaultAsync(p=>p.Id == id);
            if(isProductExist == null)
            {
                return NotFound();
            }


            return Ok(isProductExist);
        }


        // --- Add Category
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDTO NameDTO)
        {
            if (string.IsNullOrWhiteSpace(NameDTO.ToString()))
            {
                return BadRequest();
            }

            //Convert DTO into Model
            var ModelData =  _mapper.Map<Categories>(NameDTO);

            await _dbContext.Categories.AddAsync(ModelData);
            await _dbContext.SaveChangesAsync();


            //Convert Model into DTO
            var DTOResult = _mapper.Map<AddCategoryDTO>(ModelData);

            return Ok(new
            {
                Data = DTOResult,
                Message = "Category Added Successfully!"
            });


        }


        // --- Update Category
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory(int id , [FromBody] AddCategoryDTO category)
        {

            if (id == 0 || id == null || string.IsNullOrWhiteSpace(category.ToString()))
            {
                return BadRequest();
            }

            var isProductExist = await _dbContext.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (isProductExist == null)
            {
                return NotFound();
            }

               isProductExist.Name  = category.Name;
           await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                Data = isProductExist,
                Message = "Category Updated Successfully!"
            });
        }



        // --- Delete Category
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletCategory(int id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }

            var isProductExist = await _dbContext.Categories.FirstOrDefaultAsync(C => C.Id == id);
            if (isProductExist == null)
            {
                return NotFound();
            }

             _dbContext.Categories.Remove(isProductExist);

            return Ok(new
            {
             
                Data = isProductExist,
                Message = "Category Deleted Successfully!"
            });
   
        }


    }
}
