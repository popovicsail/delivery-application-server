using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Api.Contracts.Dishes
{
    public class DishDetailResponse
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
}
