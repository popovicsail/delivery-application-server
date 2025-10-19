namespace Delivery.Application.Dtos.Users.AdministratorDtos.Responses
{
    public class UserCreatedResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
