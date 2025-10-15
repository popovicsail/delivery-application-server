namespace Delivery.Api.Contracts.Profile
{
    public class ProfileUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}

