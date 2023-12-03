using EcommerceApp.Models.Domin;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApp.Models.DTO
{
    public class OrderItemDTO
    {
        //[JsonIgnore]
        public int OrderId { get; set; }

        public int ProductId { get; set; }




    }
}
