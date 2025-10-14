using Delivery.Api.Contracts;
using Delivery.Api.Contracts.Profile;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
                ProfilePictureBase64 = user.ProfilePictureBase64,
                Roles = roles.ToList()
            };


            return Ok(profileResponse);
        }

        [HttpPut("me")]
        public async Task<IActionResult> Update([FromForm] ProfileUpdateRequest updateRequest)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;
            user.Email = updateRequest.Email;


            if (updateRequest.ProfilePicture is { Length: > 0 })
            {
                var allowedMimeTypes = new[] { "image/png", "image/jpeg" };
                var contentType = updateRequest.ProfilePicture.ContentType.ToLower();

                if (!allowedMimeTypes.Contains(contentType))
                    return BadRequest("Only PNG and JPEG images are allowed.");

                await using var ms = new MemoryStream();
                await updateRequest.ProfilePicture.CopyToAsync(ms);
                var fileBytes = ms.ToArray();


                user.ProfilePictureBase64 = $"data:{contentType};base64,{Convert.ToBase64String(fileBytes)}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roles = await _userManager.GetRolesAsync(user);

            var profileResponse = new ProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureBase64 = user.ProfilePictureBase64,
                Roles = roles.ToList()
            };

            return Ok(profileResponse);
        }


    }
}
