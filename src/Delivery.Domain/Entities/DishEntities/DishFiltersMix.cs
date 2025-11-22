namespace Delivery.Domain.Entities.DishEntities;

public class DishFiltersMix
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public double? MinPrice { get; set; } = 0;
    public double? MaxPrice { get; set; }
    public bool AllergicOnAlso { get; set; } = true;
    public List<Guid>? Allergens { get; set; }
}
