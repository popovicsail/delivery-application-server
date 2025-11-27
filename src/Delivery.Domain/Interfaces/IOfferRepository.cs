using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.OfferEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<IEnumerable<Offer>> GetByRestaurantAsync(Guid restaurantId);
        Task<IEnumerable<Offer>> GetByIdsWithAllergensAsync(IEnumerable<Guid> offerIds);
    }
}
