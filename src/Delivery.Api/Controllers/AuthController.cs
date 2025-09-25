using Delivery.Api.Contracts;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Delivery.Application.Interfaces;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/auth
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")] // /api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Unauthorized("ERROR: Invalid username or password."); // Može se staviti i NotFoundResponse, ali se tako napadačima otkriva koji UserName postoji u bazi
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized("ERROR: Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles.ToList());


            return Ok(new LoginResponse
            {
                Token = token
            });
        }

        //log out

        //registration
    }
}
