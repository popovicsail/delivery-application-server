namespace Delivery.Application.Dtos.Users.CourierDtos.Responses;

public class CourierSummaryResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
