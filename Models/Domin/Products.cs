using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceApp.Models.Domin
{
    public class Products
    {

       
        public int Id { get; set; }
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
       public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public Categories Categories { get; set; }


    }
}
