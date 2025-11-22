using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.OrderEntities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Delivery.Infrastructure.Services.PdfService.PdfDocuments
{
    public class BillPdfDocument : IDocument
    {
        private readonly Bill _bill;

        public BillPdfDocument(Bill bill)
        {
            _bill = bill;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text($"Račun #{_bill.OrderId.ToString().Substring(0, 8)}")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Item().Text($"Kupac: {_bill.CustomerName}");
                        x.Item().Text($"Email: {_bill.CustomerEmail}");
                        x.Item().Text($"Datum: {_bill.IssuedAt:dd.MM.yyyy HH:mm}");

                        x.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Proizvod");
                                header.Cell().Element(CellStyle).AlignRight().Text("Količina");
                                header.Cell().Element(CellStyle).AlignRight().Text("Cena");
                                header.Cell().Element(CellStyle).AlignRight().Text("Ukupno");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                }
                            });

                            foreach (var item in _bill.Items)
                            {
                                table.Cell().Element(CellStyle).Text(item.ProductName);
                                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString());
                                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price:N2} RSD");
                                table.Cell().Element(CellStyle).AlignRight().Text($"{item.TotalPrice:N2} RSD");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).PaddingVertical(5);
                                }
                            }
                        });

                        x.Item().PaddingTop(10).AlignRight().Text($"UKUPNO: {_bill.TotalAmount:N2} RSD").SemiBold().FontSize(14);
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Hvala što koristite Delivery App! ");
                        x.CurrentPageNumber();
                    });
            });
        }
    }
}