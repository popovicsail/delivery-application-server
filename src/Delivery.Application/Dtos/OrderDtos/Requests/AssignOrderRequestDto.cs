namespace Delivery.Application.Dtos.OrderDtos.Requests;

public class AssignOrderRequestDto
{
    public Guid OrderId { get; set; }
    public Guid CourierId { get; set; }
}
