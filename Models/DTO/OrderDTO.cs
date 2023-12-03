using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Models.DTO
{
    public class OrderDTO
    {

 
        public string Address { get; set; }
      
        public double TotalAmount { get; set; }

      
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

    }
}
