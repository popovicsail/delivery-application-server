using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Api.Contracts.Helper
{
    public class DishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string? PictureUrl { get; set; }

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public virtual ICollection<DishOptionGroup>? DishOptionGroups { get; set; } = new HashSet<DishOptionGroup>();
        public virtual ICollection<Allergen>? Allergens { get; set; } = new HashSet<Allergen>();
    }
}
