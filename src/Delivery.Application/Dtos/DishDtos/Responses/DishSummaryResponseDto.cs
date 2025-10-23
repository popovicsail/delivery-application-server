namespace Delivery.Application.Dtos.DishDtos.Responses;

public class DishSummaryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string? Picture { get; set; }
    public Guid MenuId { get; set; }
}
