namespace Delivery.Api.Contracts.Dishes
{
    public class DishCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }

        public Guid MenuId { get; set; }
    }
}
