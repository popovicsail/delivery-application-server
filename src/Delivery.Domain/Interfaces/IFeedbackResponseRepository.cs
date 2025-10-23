using Delivery.Domain.Entities.FeedbackEntities;

namespace Delivery.Domain.Interfaces;

public interface IFeedbackResponseRepository : IGenericRepository<FeedbackResponse>
{
    Task<IEnumerable<FeedbackResponse>> GetResponsesByPeriodAsync(Guid questionId, DateTime from, DateTime to);
    Task<double> GetAverageRatingAsync(Guid questionId, DateTime from, DateTime to);
    Task<IEnumerable<FeedbackResponse>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<FeedbackResponse>> GetAllAsync();
    Task AddAsync(FeedbackResponse response);
}
