namespace Delivery.Application.Dtos.Users.UserDtos.Responses;

public class UserSummaryResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Roles { get; set; }
}
