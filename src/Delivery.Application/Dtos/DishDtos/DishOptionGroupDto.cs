namespace Delivery.Application.Dtos.DishDtos;

public class DishOptionGroupDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Type { get; set; }
    public List<DishOptionDto> DishOptions { get; set; } = new();
}
