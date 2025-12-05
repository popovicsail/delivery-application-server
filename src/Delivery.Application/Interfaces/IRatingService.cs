using System.Security.Claims;

using System.Security.Claims;

namespace Delivery.Application.Interfaces
{
    public interface IRatingService
    {
        Task<Guid> CreateRatingAsync(CreateRatingRequestDto dto, ClaimsPrincipal claimsPrincipal);
        Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetRatingsForRestaurantAsync(
        Guid restaurantId,
        int page,
        int pageSize,
        DateTime? from = null,
        DateTime? to = null);
        Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetRatingsForCourierAsync(
        Guid courierId,
        int page,
        int pageSize,
        DateTime? from = null,
        DateTime? to = null);

        Task<double> GetAverageRatingAsync(Guid targetId, int targetType);
    }
}
