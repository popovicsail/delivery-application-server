public class CanceledOrdersStatisticsDto
{
    public List<DailyCanceledOrdersDto> Daily { get; set; } = new();
    public int TotalCanceled { get; set; }
    public double AverageCanceledPerDay { get; set; }
}