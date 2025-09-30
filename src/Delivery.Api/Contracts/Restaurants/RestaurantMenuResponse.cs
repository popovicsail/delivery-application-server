using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Contracts.Restaurants
{
    public class RestaurantMenuResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MenuDto> Menus { get; set; } = new();
    }
}
