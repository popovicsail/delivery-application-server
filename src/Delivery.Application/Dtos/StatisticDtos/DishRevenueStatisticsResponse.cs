public class DishRevenueStatisticsResponse
{
    public List<DailyDishRevenue> Daily { get; set; } = new();
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public decimal AverageRevenue { get; set; }
}