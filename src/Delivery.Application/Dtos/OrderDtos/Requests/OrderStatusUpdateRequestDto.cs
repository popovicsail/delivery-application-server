namespace Delivery.Application.Dtos.OrderDtos.Requests;

public class OrderStatusUpdateRequestDto
{
    public int NewStatus { get; set; }
    public int PrepTime { get; set; }
}
