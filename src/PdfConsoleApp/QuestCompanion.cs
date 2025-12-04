using QuestPDF.Infrastructure;
using Delivery.Domain.Entities.OrderEntities;
using QuestPDF.Companion;
using Delivery.Infrastructure.Services.PdfService.PdfDocuments;
using Delivery.Domain.Entities.ReportEntities;
using Delivery.Infrastructure.Persistence.MongoEntities.ReportEntities;

QuestPDF.Settings.License = LicenseType.Community;

//var fakeBill = new Bill
//{
//    Id = "test-id",
//    OrderId = Guid.NewGuid(),
//    CustomerName = "Marko Marković",
//    CustomerEmail = "marko@example.com",
//    IssuedAt = DateTime.Now,
//    TotalAmount = 1250,
//    Items = new List<BillItem>
//    {
//        new BillItem { ProductName = "Pizza Margarita", Quantity = 1, Price = 900 },
//        new BillItem { ProductName = "Coca Cola 0.33", Quantity = 2, Price = 175 }
//    }
//};

//var document = new BillPdfDocument(fakeBill);

var fakeReport = new MonthlyReport
{
    Id = "report-" + Guid.NewGuid(),
    RestaurantId = Guid.NewGuid(),
    GeneratedAt = DateTime.Now,

    TotalRevenue = 253400,
    TotalOrders = 482,
    AverageRevenuePerOrder = 253400 / 482.0,

    TopOrders = new List<OrderSummary>
    {
        new OrderSummary
        {
            OrderId = Guid.NewGuid(),
            TotalPrice = 5400,
            Items = new List<OrderMealItem>
            {
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Gurmanska Pljeskavica", Price = 950, Qty = 2 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Pomfrit XL", Price = 380, Qty = 1 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Coca Cola 0.5", Price = 190, Qty = 2 },
            }
        },
        new OrderSummary
        {
            OrderId = Guid.NewGuid(),
            TotalPrice = 4700,
            Items = new List<OrderMealItem>
            {
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Capricciosa", Price = 1100, Qty = 2 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Tiramisu", Price = 650, Qty = 1 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Fanta 0.33", Price = 180, Qty = 2 },
            }
        },
        new OrderSummary
        {
            OrderId = Guid.NewGuid(),
            TotalPrice = 3950,
            Items = new List<OrderMealItem>
            {
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Pizza Quattro Formaggi", Price = 1300, Qty = 1 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Grčka Salata", Price = 650, Qty = 1 },
                new OrderMealItem { DishId = Guid.NewGuid(), Name = "Sprite 0.33", Price = 180, Qty = 2 },
            }
        }
    },

    MostPopularMeals = new List<MealCount>
    {
        new MealCount { DishId = Guid.NewGuid(), Name = "Capricciosa", Count = 142 },
        new MealCount { DishId = Guid.NewGuid(), Name = "Gurmanska Pljeskavica", Count = 118 },
        new MealCount { DishId = Guid.NewGuid(), Name = "Pomfrit", Count = 97 },
        new MealCount { DishId = Guid.NewGuid(), Name = "Coca Cola 0.33", Count = 86 }
    },

    LeastPopularMeals = new List<MealCount>
    {
        new MealCount { DishId = Guid.NewGuid(), Name = "Fish & Chips", Count = 4 },
        new MealCount { DishId = Guid.NewGuid(), Name = "Vegetarijanska Pizza", Count = 6 },
        new MealCount { DishId = Guid.NewGuid(), Name = "Supreme Burger", Count = 8 }
    }
};

var document = new ReportPdfDocument(fakeReport);

document.ShowInCompanion();