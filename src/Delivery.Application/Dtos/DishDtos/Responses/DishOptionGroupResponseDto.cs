public class DishOptionGroupResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<DishOptionResponseDto> DishOptions { get; set; } = new();
}