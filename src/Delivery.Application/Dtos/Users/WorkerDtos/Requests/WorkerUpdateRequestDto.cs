namespace Delivery.Application.Dtos.Users.WorkerDtos.Requests;

public class WorkerUpdateRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Job { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureBase64 { get; set; }
    public Guid? RestaurantId { get; set; }
}
