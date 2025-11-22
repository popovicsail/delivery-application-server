using Delivery.Application.Dtos.FeedbackDtos;

namespace Delivery.Application.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync();

        Task<IEnumerable<FeedbackResponseDto>?> GetUserFeedbackAsync(Guid userId);

        Task SubmitFeedbackAsync(Guid userId, IEnumerable<FeedbackCreateRequestDto> responses);

        Task<PagedResultDto<FeedbackResponseWithUserDto>> GetFilteredResponsesAsync(FeedbackFilterRequestDto filter);
    }
}