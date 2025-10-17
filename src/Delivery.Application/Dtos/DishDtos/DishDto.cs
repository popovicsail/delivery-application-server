namespace Delivery.Application.Dtos.DishDtos;

public class DishDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string PictureUrl { get; set; }
}