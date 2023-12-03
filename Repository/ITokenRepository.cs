using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(ApplicationUser user, List<string> Role);
    }
}
