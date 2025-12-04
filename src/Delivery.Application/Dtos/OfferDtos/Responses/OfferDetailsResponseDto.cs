namespace Delivery.Application.Dtos.OfferDtos.Responses
{
    public class OfferDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool FreeDelivery { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
        public string? Image { get; set; }
        public Guid MenuId { get; set; }
        public List<OfferDishDto> OfferDishes { get; set; } = new List<OfferDishDto>();
    }
}
