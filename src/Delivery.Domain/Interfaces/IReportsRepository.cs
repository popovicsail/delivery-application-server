using Delivery.Domain.Entities.ReportEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IReportsRepository
    {
        Task SaveReportAsync(MonthlyReport report);
        Task<MonthlyReport?> GetReportAsync(Guid restaurantId);
    }
}
