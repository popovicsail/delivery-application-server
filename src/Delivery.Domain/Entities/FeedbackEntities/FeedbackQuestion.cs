using Delivery.Domain.Entities.FeedbackEntities;

public class FeedbackQuestion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public virtual ICollection<FeedbackResponse> Responses { get; set; } = new List<FeedbackResponse>();

    public FeedbackQuestion() { }
    public FeedbackQuestion(string text)
    {
        Text = text;
    }
}