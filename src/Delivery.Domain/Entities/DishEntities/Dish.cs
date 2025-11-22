using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Entities.DishEntities;

public class Dish
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string? Picture { get; set; }
    public double DiscountRate { get; set; }
    public DateTime? DiscountExpireAt { get; set; }

    public Guid MenuId { get; set; }
    public virtual Menu Menu { get; set; }

    public virtual ICollection<DishOptionGroup>? DishOptionGroups { get; set; } = new HashSet<DishOptionGroup>();
    public virtual ICollection<Allergen>? Allergens { get; set; } = new HashSet<Allergen>();
}
