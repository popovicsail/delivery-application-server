using Delivery.Application.Dtos.DishDtos;
using Delivery.Application.Dtos.OfferDtos.Responses;

namespace Delivery.Application.Dtos.RestaurantDtos;

public class MenuDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public List<DishDto> Dishes { get; set; } = new List<DishDto>();
    public List<OfferDetailsResponseDto> Offers { get; set; } = new List<OfferDetailsResponseDto>();
}
