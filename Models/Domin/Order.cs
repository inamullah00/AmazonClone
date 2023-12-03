using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Models.Domin
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}
