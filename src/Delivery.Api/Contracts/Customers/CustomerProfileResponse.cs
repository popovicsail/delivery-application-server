using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Api.Contracts.Customers
{
    public class CustomerProfileResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<AllergenDto> Allergens { get; set; }
        public ICollection<AddressDto> Adresses { get; set; }

    }
}
