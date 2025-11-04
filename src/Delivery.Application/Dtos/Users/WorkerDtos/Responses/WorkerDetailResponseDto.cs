namespace Delivery.Application.Dtos.Users.WorkerDtos.Responses;

public class WorkerDetailResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Job { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureBase64 { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsSuspended { get; set; } = false;
    public bool isActive { get; set; } = true;
    public Guid? RestaurantId { get; set; }
}
