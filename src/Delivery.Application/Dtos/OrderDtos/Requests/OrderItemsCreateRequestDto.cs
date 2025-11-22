using Delivery.Application.Dtos.OrderDtos.Responses;

namespace Delivery.Application.Dtos.OrderDtos.Requests;

public class OrderItemsCreateRequestDto
{
    public Guid RestaurantId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
