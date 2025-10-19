using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Domain.Interfaces;

public interface IAllergenRepository : IGenericRepository<Allergen>
{
    Task<IEnumerable<Allergen>> FindAsync(IEnumerable<Guid> ids);
}
