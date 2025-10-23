using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Interfaces;

public interface IDishRepository : IGenericRepository<Dish>
{
    Task<PaginatedList<Dish>> GetPagedAsync(int sort, DishFiltersMix filterMix, int page);
    Task<Menu?> GetMenuAsync(Guid menuId);
}
