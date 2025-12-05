using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.ReportEntities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Delivery.Infrastructure.Services.PdfService.PdfDocuments
{
    public class ReportPdfDocument : IDocument
    {
        private readonly MonthlyReport _monthlyReport;

        public ReportPdfDocument(MonthlyReport report)
        {
            _monthlyReport = report;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                // HEADER
                page.Header()
                    .Text($"Mesečni Izveštaj — Restoran #{_monthlyReport.RestaurantId.ToString().Substring(0, 8)}")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                // CONTENT
                page.Content().Column(col =>
                {
                    //--------------------------------------------
                    // 1) OPŠTI PODACI
                    //--------------------------------------------
                    col.Item().PaddingBottom(10).Text("Opšti podaci")
                        .SemiBold().FontSize(16).FontColor(Colors.Black);

                    col.Item().Text($"Datum generisanja: {_monthlyReport.GeneratedAt:dd.MM.yyyy HH:mm}");
                    col.Item().Text($"Ukupna zarada: {_monthlyReport.TotalRevenue:N2} RSD");
                    col.Item().Text($"Ukupan broj porudžbina: {_monthlyReport.TotalOrders}");
                    col.Item().Text($"Prosečna zarada po porudžbini: {_monthlyReport.AverageRevenuePerOrder:N2} RSD");

                    col.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    //--------------------------------------------
                    // 2) TOP 5 PORUDŽBINA SA DETALJIMA
                    //--------------------------------------------
                    col.Item().PaddingBottom(5).Text("Top 5 porudžbina (najveći prihodi)")
                        .SemiBold().FontSize(16);

                    foreach (var order in _monthlyReport.TopOrders.Take(5))
                    {
                        col.Item().Border(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Column(orderCol =>
                        {
                            orderCol.Item().Text($"Porudžbina ID: {order.OrderId}").SemiBold();
                            orderCol.Item().Text($"Ukupna cena: {order.TotalPrice:N2} RSD");
                            orderCol.Item().Text($"Broj jela: {order.Items.Count}");

                            orderCol.Item().PaddingTop(5).Text("Stavke porudžbine:")
                                .SemiBold().FontSize(13);

                            orderCol.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Naziv jela");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Količina");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Cena");
                                });

                                foreach (var item in order.Items)
                                {
                                    table.Cell().Text(item.Name);
                                    table.Cell().AlignRight().Text(item.Qty.ToString());
                                    table.Cell().AlignRight().Text($"{item.Price:N2} RSD");
                                }
                            });
                        });

                        col.Item().PaddingBottom(10);
                    }

                    col.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    //--------------------------------------------
                    // 3) TOP 5 NAJPOPULARNIJIH JELA
                    //--------------------------------------------
                    col.Item().PaddingBottom(5).Text("Top 5 najčešće poručenih jela")
                        .SemiBold().FontSize(16);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Jelo");
                            header.Cell().Element(CellStyle).AlignRight().Text("Broj poručivanja");
                        });

                        foreach (var meal in _monthlyReport.MostPopularMeals.Take(5))
                        {
                            table.Cell().Text(meal.Name);
                            table.Cell().AlignRight().Text(meal.Count.ToString());
                        }
                    });

                    col.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    //--------------------------------------------
                    // 4) TOP 5 NAJMANJE POPULARNIH JELA
                    //--------------------------------------------
                    col.Item().PaddingBottom(5).Text("Top 5 najređe poručenih jela")
                        .SemiBold().FontSize(16);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Jelo");
                            header.Cell().Element(CellStyle).AlignRight().Text("Broj poručivanja");
                        });

                        foreach (var meal in _monthlyReport.LeastPopularMeals.Take(5))
                        {
                            table.Cell().Text(meal.Name);
                            table.Cell().AlignRight().Text(meal.Count.ToString());
                        }
                    });

                });

                // FOOTER
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Hvala što koristite naš sistem za izveštaje — ");
                        text.CurrentPageNumber();
                    });
            });
        }

        // CELL STYLE HELPER
        private static IContainer CellStyle(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.SemiBold())
                .PaddingVertical(5)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten1);
        }
    }
}
