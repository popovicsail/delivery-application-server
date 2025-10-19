using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Entities.UserEntities;

public class Owner
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
}
