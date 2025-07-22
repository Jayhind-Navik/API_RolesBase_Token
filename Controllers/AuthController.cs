using API_RolesBase_Token.Models;
using API_RolesBase_Token.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_RolesBase_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService tokenService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
        }
        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterDto model)
        //{
        //    var user = new AppUser()
        //    {
        //        UserName = model.Username,
        //        Email = model.Email,
        //        // PasswordHash = model.Password
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        await userManager.AddToRoleAsync(user, "User");
        //    }
        //    else
        //    {
        //        return BadRequest(result.Errors);
        //    }
        //    return Ok("Registration Successfully!!..");
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            // Check if role exists
            if (!await roleManager.RoleExistsAsync(model.Role))
            {
                return BadRequest($"Role '{model.Role}' does not exist.");
            }

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, model.Role);

            return Ok($"User registered with role: {model.Role}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Credentials!!..");
            }
            var resul = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (resul.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                var token = tokenService.CreateToken(user, roles);
                return Ok(new { token });
            }
            return Unauthorized("Invalid Credentials!!...");
        }
    }
}
