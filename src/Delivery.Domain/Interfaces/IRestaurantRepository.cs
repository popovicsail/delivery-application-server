using Delivery.Domain.Common;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Interfaces;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<PaginatedList<Restaurant>> GetPagedAsync(int sort, RestaurantFiltersMix filterMix, int page);
    Task<IEnumerable<Restaurant>> GetMyAsync(Guid ownerId);
    Task<Restaurant?> GetOneAsync(Guid id);
    Task<Menu> GetMenuAsync(Guid id);

}
