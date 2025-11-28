public class DishRevenueStatisticsResponse
{
    public List<DailyDishRevenue> Daily { get; set; } = new();
    public double TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public double AverageRevenue { get; set; }
}