namespace Delivery.Api.Contracts.Helper
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<DishDto> Dishes { get; set; } = new();
    }
}
