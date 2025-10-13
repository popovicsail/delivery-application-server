namespace Delivery.Api.Contracts.Profile
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? ProfilePictureBase64 { get; set; }
        public string? ProfilePictureMimeType { get; set; }

        public List<string> Roles { get; set; }
    }
}
