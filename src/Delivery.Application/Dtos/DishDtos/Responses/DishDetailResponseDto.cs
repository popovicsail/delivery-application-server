using Delivery.Application.Dtos.CommonDtos.AllergenDtos;

namespace Delivery.Application.Dtos.DishDtos.Responses;

public class DishDetailResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string PictureUrl { get; set; }

    public Guid MenuId { get; set; }

    public virtual ICollection<DishOptionGroupDto>? DishOptionGroups { get; set; } = new HashSet<DishOptionGroupDto>();
    public virtual ICollection<AllergenDto>? Allergens { get; set; } = new HashSet<AllergenDto>();
}
