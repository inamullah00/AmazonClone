using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models.DTO
{
    public class ProductsDTO
    {
       

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Size { get; set; }

        [Required]
        public int categoryId { get; set; }
        
    }
}
