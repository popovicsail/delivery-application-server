namespace Delivery.Application.Dtos.FeedbackDtos;

public class FeedbackResponseDto
{
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
