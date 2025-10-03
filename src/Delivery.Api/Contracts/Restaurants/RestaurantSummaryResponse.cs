using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.HelperEntities;

namespace Delivery.Api.Contracts.Restaurants
{
    public class RestaurantSummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public OwnerDto Owner { get; set; }
        public AddressDto Address { get; set; }
        public BaseWorkSchedDto BaseWorkSched { get; set; }
    }
}
