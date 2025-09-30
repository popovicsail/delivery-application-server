using Delivery.Api.Contracts;
using Delivery.Api.Contracts.Profile;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ProfileController(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetById()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var profileResponse = new ProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList()
            };

            return Ok(profileResponse);
        }

        [HttpPut("my-profile")]
        public async Task<IActionResult> Update([FromBody] ProfileUpdateRequest updateRequest)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("ERROR: User not found.");
            }

            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;

            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var profileResponse = new ProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList()
            };

            return Ok(profileResponse);
        }
    }
}
