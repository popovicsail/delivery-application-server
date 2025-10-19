public class DishOptionGroupUpdateRequestDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public List<DishOptionUpdateRequestDto> DishOptions { get; set; } = new();
}
