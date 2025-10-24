using Delivery.Domain.Entities.FeedbackEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class FeedbackResponseRepository : GenericRepository<FeedbackResponse>, IFeedbackResponseRepository
{
    public FeedbackResponseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<FeedbackResponse>> GetResponsesByPeriodAsync(Guid questionId, DateTime from, DateTime to)
    {
        return await _dbContext.FeedbackResponses
            .Where(r => r.QuestionId == questionId && r.CreatedAt >= from && r.CreatedAt <= to)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingAsync(Guid questionId, DateTime from, DateTime to)
    {
        var ratings = await _dbContext.FeedbackResponses
            .Where(r => r.QuestionId == questionId && r.CreatedAt >= from && r.CreatedAt <= to)
            .Select(r => r.Rating)
            .ToListAsync();

        return ratings.Count == 0 ? 0 : ratings.Average();
    }

    public async Task<IEnumerable<FeedbackResponse>> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.FeedbackResponses
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    Task IFeedbackResponseRepository.AddAsync(FeedbackResponse response)
    {
        return AddAsync(response);
    }
}
