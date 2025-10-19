using Delivery.Api.Contracts.Auth;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Domain.Entities.UserEntities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; } = DefaultAvatar.Base64;

}
