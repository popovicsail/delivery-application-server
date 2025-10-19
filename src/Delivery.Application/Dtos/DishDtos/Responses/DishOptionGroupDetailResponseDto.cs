namespace Delivery.Application.Dtos.DishDtos.Responses;

public class DishOptionGroupDetailResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public Guid DishId { get; set; }

    public List<DishOptionResponseDto> DishOptions { get; set; } = new();
}
