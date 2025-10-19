namespace Delivery.Application.Dtos.Users.WorkerDtos.Responses;

public class WorkerDetailResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
