using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.ReportEntities;
using Delivery.Infrastructure.Services.PdfService.PdfDocuments;
using QuestPDF.Fluent;

namespace Delivery.Infrastructure.Services.PdfService;

public class PdfService : IPdfService
{
    public byte[] GenerateBillPdf(Bill bill)
    {
        var document = new BillPdfDocument(bill);
        return document.GeneratePdf();
    }

    public byte[] GenerateReportPdf(MonthlyReport report)
    {
        var document = new ReportPdfDocument(report);
        return document.GeneratePdf();
    }
}