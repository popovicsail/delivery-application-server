using Delivery.Domain.Common;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Interfaces;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    Task<PaginatedList<Restaurant>> GetPagedAsync(int sort, RestaurantFiltersMix filterMix, int page);
    Task<IEnumerable<Restaurant>> GetMyAsync(Guid ownerId);
    Task<Menu> GetMenuAsync(Guid id);
}
