namespace Delivery.Application.Dtos.Users.AdministratorDtos.Requests;

public class RegisterCourierRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
