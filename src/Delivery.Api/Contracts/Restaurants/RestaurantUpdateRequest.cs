using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Contracts.Restaurants
{
    public class RestaurantUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        public Guid OwnerId { get; set; }
    }
}
