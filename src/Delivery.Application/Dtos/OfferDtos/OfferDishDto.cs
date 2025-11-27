using Delivery.Application.Dtos.DishDtos;

namespace Delivery.Application.Dtos.OfferDtos
{
    public class OfferDishDto
    {
        public Guid DishId { get; set; }
        public DishDto? Dish { get; set; }
        public int Quantity { get; set; } = 1;
    }
}