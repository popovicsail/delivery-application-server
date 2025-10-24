using Microsoft.AspNetCore.Http;

namespace Delivery.Application.Dtos.Users.ProfileDtos.Requests;

public class ProfileUpdateRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IFormFile? ProfilePictureBase64 { get; set; }
}

