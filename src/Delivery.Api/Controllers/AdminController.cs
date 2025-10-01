using Delivery.Api.Contracts.Admin;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("register-courier")]
        public async Task<IActionResult> RegisterCourier([FromBody] RegisterCourierRequest request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(newUser, "Courier");

            var courierProfile = new Courier
            {
                UserId = newUser.Id,
                WorkStatus = "NEAKTIVAN"
            };
            _context.Couriers.Add(courierProfile);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("register-owner")]
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerRequest request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(newUser, "Owner");

            var ownerProfile = new Owner
            {
                UserId = newUser.Id
            };
            _context.Owners.Add(ownerProfile);
            await _context.SaveChangesAsync();


            return Ok();
        }
    }
}
