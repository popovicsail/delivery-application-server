using Delivery.Domain.Entities.DishEntities;

namespace Delivery.Domain.Entities.OfferEntities
{
    public class OfferDish
    {
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public Offer? Offer { get; set; }
        public Guid DishId { get; set; }
        public Dish? Dish { get; set; }

        public int Quantity { get; set; } = 1;
    }
}