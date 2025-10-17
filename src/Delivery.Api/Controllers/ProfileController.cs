using System.Security.Claims;
using Delivery.Application.Dtos.Users.ProfileDtos.Requests;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetOneAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var profile = await _profileService.GetOneAsync(Guid.Parse(userId));

            return Ok(profile);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProfileUpdateRequestDto updateRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var response = await _profileService.UpdateAsync(Guid.Parse(userId), updateRequest);

            return Ok(response);
        }
    }
}
