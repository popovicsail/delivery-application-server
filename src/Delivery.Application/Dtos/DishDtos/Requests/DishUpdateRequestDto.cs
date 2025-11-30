using System.ComponentModel.DataAnnotations;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos;

namespace Delivery.Application.Dtos.DishDtos.Requests;

public class DishUpdateRequestDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public string Type { get; set; }
    [Range(0, 100)]
    public double DiscountAmount { get; set; } = 0;
    public DateTime? DiscountExpireAt { get; set; }
    public List<Guid>? AllergenIds { get; set; }
}