namespace Delivery.Api.Contracts.Helper
{
    public class OwnerDto
    {
        public Guid Id { get; set; } // OwnerId
        public Guid UserId { get; set; } // UserId
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
