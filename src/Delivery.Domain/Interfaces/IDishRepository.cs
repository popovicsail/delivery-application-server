using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Interfaces;

public interface IDishRepository : IGenericRepository<Dish>
{
    Task<PaginatedList<Dish>> GetPagedAsync(string sort, DishFiltersMix filterMix, int page);
    Task<IEnumerable<Dish>> GetAllFilteredAsync(DishFiltersMix filterMix, string sort);
    Task<Menu?> GetMenuAsync(Guid menuId);
    Task<IEnumerable<Dish>> GetByIdsWithAllergensAsync(IEnumerable<Guid> dishIds);
    Task<ICollection<DishOption>> GetDishOptionsByIdsAsync(IEnumerable<Guid> optionIds);
    Task<List<Guid>> GetManyIdsAsync(List<Guid> dishIds);
}
