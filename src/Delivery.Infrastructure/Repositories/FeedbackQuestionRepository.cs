using Delivery.Domain.Entities.FeedbackEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class FeedbackQuestionRepository : GenericRepository<FeedbackQuestion>, IFeedbackQuestionRepository
{
    public FeedbackQuestionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
