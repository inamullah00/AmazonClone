using EcommerceApp.Data;
using EcommerceApp.Models.Domin;
using EcommerceApp.Models.DTO;
using EcommerceApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ITokenRepository _TokenRepository;
        public readonly EcommerceDBContext _dbContext;
        public AuthController(UserManager<ApplicationUser> manager , ITokenRepository tokenRepository , EcommerceDBContext dbContext)
        {
            _userManager = manager;
            _TokenRepository = tokenRepository;
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("Register")]

        public async  Task<IActionResult> Register([FromBody] RegisterDTO DTOData)
        {

            var userIdentity = new ApplicationUser
            {
                UserName = DTOData.userName,
                Email = DTOData.userName

            };
           
            var RegisterdUser =   await _userManager.CreateAsync(userIdentity , DTOData.Password);
                     
            if (RegisterdUser.Succeeded)
            {
                    if(DTOData.Role != null && DTOData.Role.Any()) {

                  var AssignedRole =   await _userManager.AddToRoleAsync(userIdentity, DTOData.Role);
                  
                    if (AssignedRole.Succeeded)
                    {
                        return Ok("User Registerd Sucessfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to assign roles to the user.");
                    }
                }
            }
         
                return BadRequest(RegisterdUser);
        }





        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO DTOData)
        {

            // Check if username or password is null or empty
            if (string.IsNullOrWhiteSpace(DTOData.userName) || string.IsNullOrWhiteSpace(DTOData.password))
            {
                return BadRequest("Username or Password cannot be empty.");
            }

            // check Email or Password

           
               var user =  await _userManager.FindByEmailAsync(DTOData.userName);

                if (user != null)
                {

                    var userPassword  = await _userManager.CheckPasswordAsync(user, DTOData.password);

                    if (userPassword != null)
                    {

                        //get user Role

                        var userRole = await _userManager.GetRolesAsync(user);

                        //Create Token

                        var token = _TokenRepository.CreateToken(user,userRole.ToList());

                        return Ok(new
                        {
                            Token =   token   ,
                            Roles = userRole,

                        });


                    }
                    return BadRequest("UserName or Password Incorrect!");
                }
             
           
            
            return NotFound("User Not Exist with this Email");

        }



        //Get User 

        [HttpGet]
        [Route("User")]

        public async Task<IActionResult> User(string id)
        {
             var user = await _userManager.Users
               .Include(u => u.Orders) // Assuming you have a navigation property 'Orders' in ApplicationUser
               .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return Ok(new
            {
              
                userOrder = user.Orders
            });
        }

    }
}


