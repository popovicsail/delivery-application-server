namespace Delivery.Domain.Interfaces
{ 
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetByTargetAsync(
            Guid targetId,
            RatingTargetType targetType,
            int page,
            int pageSize,
            DateTime? from = null,
            DateTime? to = null
        );
        Task<double> GetAverageScoreAsync(Guid targetId, RatingTargetType targetType);
        Task SaveAsync(Rating rating);


    }
}
