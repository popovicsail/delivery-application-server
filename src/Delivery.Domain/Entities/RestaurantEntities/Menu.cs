using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.OfferEntities;

namespace Delivery.Domain.Entities.RestaurantEntities;

public class Menu
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new HashSet<Dish>();
    public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();
}
