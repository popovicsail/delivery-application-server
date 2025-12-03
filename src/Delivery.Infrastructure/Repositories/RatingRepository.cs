using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    internal class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {

        public RatingRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {

        }
        public async Task SaveAsync(Rating rating)
        {
            _dbContext.Rating.Add(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetByTargetAsync(
            Guid targetId,
            RatingTargetType targetType,
            int page,
            int pageSize,
            DateTime? from = null,
            DateTime? to = null
        )
        {
            var query = _dbContext.Rating
                .Where(r => r.TargetId == targetId && r.TargetType == targetType);

            if (from.HasValue)
                query = query.Where(r => r.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(r => r.CreatedAt <= to.Value);

            var totalCount = await query.CountAsync();

            var ratings = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (ratings, totalCount);
        }

        public async Task<double> GetAverageScoreAsync(Guid targetId, RatingTargetType targetType)
        {
            return await _dbContext.Rating
                .Where(r => r.TargetId == targetId && r.TargetType == targetType)
                .AverageAsync(r => r.Score);
        }
    }
}
