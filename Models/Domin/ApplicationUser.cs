using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Models.Domin
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
