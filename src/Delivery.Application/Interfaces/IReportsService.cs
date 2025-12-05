using Delivery.Domain.Entities.ReportEntities;

namespace Delivery.Application.Interfaces
{
    public interface IReportsService
    {
        Task<MonthlyReport> GenerateAndSaveMonthlyReportAsync(Guid restaurantId, int daysBack = 30);
        Task<byte[]> GenerateAndSaveReportPdfAsync(Guid restaurantId, int daysBack = 30);
        Task<MonthlyReport?> GetLatestSavedReportAsync(Guid restaurantId);
    }
}
