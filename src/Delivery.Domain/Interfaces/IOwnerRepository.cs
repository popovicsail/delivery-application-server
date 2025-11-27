using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces;

public interface IOwnerRepository : IGenericRepository<Owner>
{
    Task<Owner?> GetByUserIdAsync(Guid userId);
    Task<bool> GetRestaurantPermissionAsync(User user, Guid restaurantId);
}
