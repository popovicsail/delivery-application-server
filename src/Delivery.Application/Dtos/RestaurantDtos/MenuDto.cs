using Delivery.Application.Dtos.DishDtos;

namespace Delivery.Application.Dtos.RestaurantDtos;

public class MenuDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DishDto> Dishes { get; set; } = new();
}
