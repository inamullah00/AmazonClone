using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models.DTO
{
    public class RegisterDTO
    {
        
        
        [DataType(DataType.EmailAddress)]       
        public string userName { get; set; }
        [DataType(DataType.Password)]
       public string Password { get; set; }
       
        public string Role { get; set; }

    }
}
