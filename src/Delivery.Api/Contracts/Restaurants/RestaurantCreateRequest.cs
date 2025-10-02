using System.ComponentModel.DataAnnotations;
using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Contracts.Restaurants
{
    public class RestaurantCreateRequest
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}
