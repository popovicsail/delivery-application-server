using QuestPDF.Infrastructure;
using Delivery.Domain.Entities.OrderEntities;
using QuestPDF.Companion;
using Delivery.Infrastructure.Services.PdfService.PdfDocuments;

QuestPDF.Settings.License = LicenseType.Community;

var fakeBill = new Bill
{
    Id = "test-id",
    OrderId = Guid.NewGuid(),
    CustomerName = "Marko Marković",
    CustomerEmail = "marko@example.com",
    IssuedAt = DateTime.Now,
    TotalAmount = 1250,
    Items = new List<BillItem>
    {
        new BillItem { ProductName = "Pizza Margarita", Quantity = 1, Price = 900 },
        new BillItem { ProductName = "Coca Cola 0.33", Quantity = 2, Price = 175 }
    }
};

var document = new BillPdfDocument(fakeBill);

document.ShowInCompanion();