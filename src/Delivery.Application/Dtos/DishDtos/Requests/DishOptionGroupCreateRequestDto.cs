using Delivery.Application.Dtos.DishDtos.Requests;

public class DishOptionGroupCreateRequestDto
{
    public string Name { get; set; }
    public Guid DishId { get; set; }
    public string Type { get; set; }
    public List<DishOptionCreateDto> DishOptions { get; set; } = new();
}
