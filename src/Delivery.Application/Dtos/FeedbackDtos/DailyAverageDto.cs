public class FeedbackStatsDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int TotalResponses { get; set; }
    public IEnumerable<DailyAverageDto> DailyAverages { get; set; } = [];
}