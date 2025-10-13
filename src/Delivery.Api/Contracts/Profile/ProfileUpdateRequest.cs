namespace Delivery.Api.Contracts.Profile
{
    public class ProfileUpdateRequest
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email  { get; set; }
        public string? ProfilePictureBase64 { get; set; }
        public string? ProfilePictureMimeType { get; set; }

    }
}

