using Delivery.Domain.Common;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    Task<PaginatedList<Restaurant>> GetPagedAsync(int sort, RestaurantFiltersMix filterMix, int page);
    Task<IEnumerable<Restaurant>> GetMyAsync(Guid ownerId);
    Task<IEnumerable<Restaurant>> GetTopRatedAsync();
    Task<IEnumerable<Restaurant>> GetWithMostDiscountsAsync();
    Task<IEnumerable<Restaurant>> GetMostOftenOrderedFromByCustomerAsync(Guid customerId);
    Task<IEnumerable<Restaurant>> GetMostRecentOrderedFromByCustomerAsync(Guid customerId);
    Task<IEnumerable<Worker>> GetWorkersAsync(Guid restaurantId);
    Task<Menu> GetMenuAsync(Guid id);
}
