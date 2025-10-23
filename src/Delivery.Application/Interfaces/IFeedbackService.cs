using Delivery.Application.Dtos.FeedbackDtos;

namespace Delivery.Application.Interfaces
{
    public interface IFeedbackService
    {
        // Vrati sva pitanja
        Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync();

        // Vrati feedback odgovore korisnika ako postoje
        Task<IEnumerable<FeedbackResponseDto>?> GetUserFeedbackAsync(Guid userId);

        // Kreiraj ili ažuriraj feedback (zavisi da li korisnik već ima)
        Task SubmitFeedbackAsync(Guid userId, IEnumerable<FeedbackResponseDto> responses);

        // Statistika po pitanjima
        Task<IEnumerable<FeedbackStatsDto>> GetStatisticsAsync();
    }
}
