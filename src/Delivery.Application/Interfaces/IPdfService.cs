using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.ReportEntities;

namespace Delivery.Application.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateBillPdf(Bill bill);
        byte[] GenerateReportPdf(MonthlyReport report);
    }
}
