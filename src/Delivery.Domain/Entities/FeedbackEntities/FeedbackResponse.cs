using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.FeedbackEntities;

public class FeedbackResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid QuestionId { get; set; }
    public int Rating { get; set; } // 1–5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual FeedbackQuestion? Question { get; set; }

    public FeedbackResponse() { }

    public FeedbackResponse(Guid questionId, int rating, string? comment = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5");

        QuestionId = questionId;
        Rating = rating;
        Comment = comment;
    }
}
