using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user, List<string> roles);
}
