namespace Delivery.Application.Dtos.DishDtos.Requests;

public class DishCreateRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string? Picture { get; set; }

    public Guid MenuId { get; set; }
}