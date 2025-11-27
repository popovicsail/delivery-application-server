using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Entities.OfferEntities
{
    public class Offer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool FreeDelivery { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
        public string? Image { get; set; }
        public Guid MenuId { get; set; }
        public Menu? Menu { get; set; }
        public ICollection<OfferDish> OfferDishes { get; set; } = new List<OfferDish>();
    }
}
