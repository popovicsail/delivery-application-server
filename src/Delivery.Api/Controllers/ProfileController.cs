using Delivery.Api.Contracts;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
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

        [HttpPatch("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] JsonPatchDocument<ProfilePatchRequest> patchRequest)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userPatchTest = new ProfilePatchRequest
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            patchRequest.ApplyTo(userPatchTest);

            if (!TryValidateModel(userPatchTest))
            {
                return ValidationProblem(ModelState);
            }

            user.FirstName = userPatchTest.FirstName;
            user.LastName = userPatchTest.LastName;

            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
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
    }
}
