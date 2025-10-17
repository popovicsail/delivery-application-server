using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class AllergenRepository : GenericRepository<Allergen>, IAllergenRepository
{
    public AllergenRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
