public class RestaurantRevenueStatisticsDto
{
    public List<RestaurantDailyRevenueDto> Daily { get; set; }
    public double TotalRevenue { get; set; }
    public int OrderCount { get; set; }
    public double AverageOrderValue { get; set; }
}
