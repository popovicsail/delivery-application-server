using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class CreateRatingRequestDto
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TargetId { get; set; }
    public RatingTargetType TargetType { get; set; }

    [Range(1, 5)]
    public int Score { get; set; }

    public string? Comment { get; set; }

    public IFormFile? Image { get; set; }
}
