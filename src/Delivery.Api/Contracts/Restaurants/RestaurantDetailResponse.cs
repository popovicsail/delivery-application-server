using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Api.Contracts.Restaurants
{
    public class RestaurantDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AddressDto Address { get; set; }

        public OwnerDto Owner { get; set; }
    }
}
