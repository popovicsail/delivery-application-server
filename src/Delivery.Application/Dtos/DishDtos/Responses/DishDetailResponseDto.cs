public class DishDetailResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string? Picture { get; set; }

    public List<DishOptionGroupResponseDto> DishOptionGroups { get; set; } = new();
}