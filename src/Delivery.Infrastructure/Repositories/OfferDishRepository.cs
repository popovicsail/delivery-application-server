using Delivery.Domain.Entities.OfferEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories
{
    public class OfferDishRepository : GenericRepository<OfferDish>, IOfferDishRepository
    {
        public OfferDishRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
