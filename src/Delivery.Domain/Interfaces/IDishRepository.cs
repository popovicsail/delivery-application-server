using Delivery.Domain.Entities.DishEntities;

namespace Delivery.Domain.Interfaces;

public interface IDishRepository : IGenericRepository<Dish>
{
    Task<IEnumerable<Dish>> GetByIdsWithAllergensAsync(IEnumerable<Guid> dishIds);
}
