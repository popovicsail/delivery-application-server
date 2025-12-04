using Delivery.Domain.Entities.OfferEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<IEnumerable<Offer>> GetByRestaurantAsync(Guid restaurantId);
        Task<IEnumerable<Offer>> GetByIdsWithAllergensAsync(IEnumerable<Guid> offerIds);
    }
}
