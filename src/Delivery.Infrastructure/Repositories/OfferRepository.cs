using Delivery.Domain.Entities.OfferEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Offer>> GetByRestaurantAsync(Guid restaurantId)
        {
            return await _dbContext.Offers
                .Include(o => o.Menu)
                .Include(o => o.OfferDishes)
                .Where(o => o.Menu!.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public new async Task<Offer?> GetOneAsync(Guid id)
        {
            return await _dbContext.Offers
                .Include(o => o.OfferDishes)
                    .ThenInclude(od => od.Dish)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Offer>> GetByIdsWithAllergensAsync(IEnumerable<Guid> offerIds)
        {
            return await _dbContext.Offers
                .AsNoTracking()
                .Where(o => offerIds.Contains(o.Id))
                .Include(o => o.OfferDishes)
                    .ThenInclude(od => od.Dish)
                        .ThenInclude(d => d.Allergens)
                .ToListAsync();
        }
    }
}
