namespace Delivery.Application.Dtos.FeedbackDtos
{
    public class FeedbackResponseWithUserDto
    {
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
