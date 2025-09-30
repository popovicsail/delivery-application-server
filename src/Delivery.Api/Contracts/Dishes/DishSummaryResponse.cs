namespace Delivery.Api.Contracts.Dishes
{
    public class DishSummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
