using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceApp.Repository
{
    public class TokenRepository : ITokenRepository
    {

        public readonly IConfiguration Configuration;
        public TokenRepository(IConfiguration configuration)
        {
              Configuration = configuration;
        }
        public string CreateToken(ApplicationUser user, List<string> Role)
        {
            //Create Claim 

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email , user.Email));

            foreach (var role in Role)
            {
                claims.Add(new Claim (ClaimTypes.Role, role));
            }

            // Symetery Key

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTToken:Key"]));


            //Signin Key

            var signInKey = new SigningCredentials(key ,SecurityAlgorithms.HmacSha256);

            //Create Token


            var JwtToken = new JwtSecurityToken
            (

              Configuration["JWTToken:Issuer"],
               Configuration["JWTToken:Audience"],
               claims : claims    ,
               signingCredentials : signInKey,
               expires:DateTime.Now.AddMinutes(10)

            );

            var tokenHandler = new JwtSecurityTokenHandler();

            
            return tokenHandler.WriteToken(JwtToken);





        }
    }
}
