using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Dtos.OrderDtos.Responses;

public class OrderResponseDto
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string DeliveryAddress { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public Restaurant Restaurant { get; set; }
    public int TimeToPrepare { get; set; }
    public List<OrderItemSummaryResponse> Items { get; set; } = new List<OrderItemSummaryResponse>();
    public string Status { get; set; } // npr. "KREIRANA"
}
