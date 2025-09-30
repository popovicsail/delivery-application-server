using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Contracts.Profile
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<string> Roles { get; set; }

        public AllergenDto Allergens { get; set; }
    }
}
