using Delivery.Application.Dtos.DishDtos;

namespace Delivery.Application.Dtos.OrderDtos.Responses;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public List<DishOptionGroupDto> DishOptionGroups { get; set; } = new();
}
