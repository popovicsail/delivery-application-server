using Delivery.Application.Dtos.CommonDtos.AllergenDtos;

namespace Delivery.Application.Dtos.DishDtos;

public class DishDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public double DiscountRate { get; set; }
    public DateTime? DiscountExpireAt { get; set; }
    public string? Picture { get; set; }
    public List<AllergenDto>? Allergens { get; set; } = new List<AllergenDto>();
    public List<DishOptionGroupDto>? DishOptionGroups { get; set; } = new List<DishOptionGroupDto>();
}