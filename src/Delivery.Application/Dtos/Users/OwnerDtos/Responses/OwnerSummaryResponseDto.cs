namespace Delivery.Application.Dtos.Users.OwnerDtos.Responses;

public class OwnerSummaryResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
