using System;
using System.Threading.Tasks;
using Delivery.Api.Contracts.Auth;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("login")]
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

            var loginResponse = new LoginResponse
            {
                Token = token
            };

            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            User user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest(createResult.Errors);
            }


            if (request.Role == null)
            {
                var identityResult = await _userManager.AddToRoleAsync(user, "Customer");
                if (!identityResult.Succeeded)
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            else
            {
                var identityResult = await _userManager.AddToRoleAsync(user, request.Role);
                if (!identityResult.Succeeded)
                {
                    return BadRequest(identityResult.Errors);
                }
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles.ToList());

            var registerResponse = new RegisterResponse
            {
                Token = token
            };

            return Ok(registerResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
