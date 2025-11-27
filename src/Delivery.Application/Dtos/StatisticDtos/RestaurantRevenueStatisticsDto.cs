public class RestaurantRevenueStatisticsDto
{
    public List<RestaurantDailyRevenueDto> Daily { get; set; }
    public decimal TotalRevenue { get; set; }
    public int OrderCount { get; set; }
    public decimal AverageOrderValue { get; set; }
}
