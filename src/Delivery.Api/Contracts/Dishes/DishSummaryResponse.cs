namespace Delivery.Api.Contracts.Dishes
{
    public class DishSummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
