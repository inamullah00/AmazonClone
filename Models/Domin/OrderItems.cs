using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models.Domin
{
    public class OrderItems
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }                                                     

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }

    }
}
