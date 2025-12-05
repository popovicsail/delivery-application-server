namespace Delivery.Application.Dtos.Users.AuthDtos.Requests;

public class ActivateAccountRequestDto
{
    public string Email { get; set; }
    public string Token { get; set; }
}