using Delivery.Domain.Entities.OrderEntities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }

    public Guid TargetId { get; set; } // RestaurantId or CourierId
    public RatingTargetType TargetType { get; set; } // Enum

    public int Score { get; set; } // 1–5
    public string? Comment { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Order Order { get; set; }
}
