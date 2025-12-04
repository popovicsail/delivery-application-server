using Delivery.Domain.Entities.ReportEntities;

namespace Delivery.Infrastructure.Persistence.MongoEntities.ReportEntities.Mappers
{
    public static class MonthlyReportMapper
    {
        public static MonthlyReport ToDomain(MonthlyReportDocument doc)
        {
            if (doc == null) return null;

            return new MonthlyReport
            {
                Id = doc.Id,
                RestaurantId = doc.RestaurantId,
                GeneratedAt = doc.GeneratedAt,
                TotalRevenue = doc.TotalRevenue,
                TotalOrders = doc.TotalOrders,
                AverageRevenuePerOrder = doc.AverageRevenuePerOrder,
                TopOrders = doc.TopOrders?.Select(o => new OrderSummary
                {
                    OrderId = o.OrderId,
                    TotalPrice = o.TotalPrice,
                    Items = o.Items?.Select(i => new OrderMealItem
                    {
                        DishId = i.DishId,
                        Name = i.Name,
                        Price = i.Price,
                        Qty = i.Qty
                    }).ToList()
                }).ToList(),
                MostPopularMeals = doc.MostPopularMeals?.Select(m => new MealCount
                {
                    DishId = m.DishId,
                    Name = m.Name,
                    Count = m.Count
                }).ToList(),
                LeastPopularMeals = doc.LeastPopularMeals?.Select(m => new MealCount
                {
                    DishId = m.DishId,
                    Name = m.Name,
                    Count = m.Count
                }).ToList()
            };
        }

        public static MonthlyReportDocument ToDocument(MonthlyReport domain)
        {
            if (domain == null) return null;

            return new MonthlyReportDocument
            {
                Id = domain.Id,
                RestaurantId = domain.RestaurantId,
                GeneratedAt = domain.GeneratedAt,
                TotalRevenue = domain.TotalRevenue,
                TotalOrders = domain.TotalOrders,
                AverageRevenuePerOrder = domain.AverageRevenuePerOrder,
                TopOrders = domain.TopOrders?.Select(o => new OrderSummaryDocument
                {
                    OrderId = o.OrderId,
                    TotalPrice = o.TotalPrice,
                    Items = o.Items?.Select(i => new OrderMealItemDocument
                    {
                        DishId = i.DishId,
                        Name = i.Name,
                        Price = i.Price,
                        Qty = i.Qty
                    }).ToList()
                }).ToList(),
                MostPopularMeals = domain.MostPopularMeals?.Select(m => new MealCountDocument
                {
                    DishId = m.DishId,
                    Name = m.Name,
                    Count = m.Count
                }).ToList(),
                LeastPopularMeals = domain.LeastPopularMeals?.Select(m => new MealCountDocument
                {
                    DishId = m.DishId,
                    Name = m.Name,
                    Count = m.Count
                }).ToList()
            };
        }
    }
}
