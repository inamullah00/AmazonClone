using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models.DTO
{
    public class LoginDTO
    {

        [DataType(DataType.EmailAddress)]
        public string userName { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
