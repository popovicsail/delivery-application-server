namespace Delivery.Application.Dtos.Users.ProfileDtos.Responses;

public class ProfileResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureBase64 { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string? Status { get; set; }
}
