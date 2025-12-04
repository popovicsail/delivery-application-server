using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.ReportEntities;
using Delivery.Domain.Interfaces;

namespace Delivery.Application.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportsRepository _reportsRepository;
        private readonly IPdfService _pdfService;

        public ReportsService(
        IUnitOfWork unitOfWork,
        IReportsRepository reportsRepository,
        IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _reportsRepository = reportsRepository;
            _pdfService = pdfService;
        }

        public async Task<MonthlyReport> GenerateAndSaveMonthlyReportAsync(Guid restaurantId, int daysBack = 30)
        {
            var toDate = DateTime.UtcNow;
            var fromDate = toDate.AddDays(-daysBack);

            var orders = (await _unitOfWork.Orders.GetByRestaurantAndDateRangeAsync(restaurantId, fromDate, toDate))
                        
                .ToList();

            double totalRevenue = orders.Sum(o => o.TotalPrice);
            int totalOrders = orders.Count;
            double averageRevenue = totalOrders == 0 ? 0 : totalRevenue / totalOrders;

            var topOrders = orders
                .OrderByDescending(o => o.TotalPrice)
                .Take(5)
                .Select(o => new OrderSummary
                {
                    OrderId = o.Id,
                    TotalPrice = o.TotalPrice,
                    Items = o.Items.Select(i => new OrderMealItem
                    {
                        DishId = i.DishId ?? Guid.Empty,
                        Name = i.Name ?? i.Dish?.Name ?? "Unknown",
                        Price = i.DishPrice + i.OptionsPrice,
                        Qty = i.Quantity
                    }).ToList()
                })
                .ToList();

            var mealCounts = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.DishId)
                .Select(g =>
                {
                    var firstItem = g.First();
                    var name = firstItem.Name ?? firstItem.Dish?.Name ?? "Unknown";
                    return new MealCount
                    {
                        DishId = g.Key ?? Guid.Empty,
                        Name = name,
                        Count = g.Sum(i => i.Quantity)
                    };
                })
                .ToList();

            var mostPopular = mealCounts
                .OrderByDescending(m => m.Count)
                .Take(5)
                .ToList();

            var leastPopular = mealCounts
                .OrderBy(m => m.Count)
                .Take(5)
                .ToList();

            var report = new MonthlyReport
            {
                Id = null, // Mongo će generisati
                RestaurantId = restaurantId,
                GeneratedAt = DateTime.UtcNow,
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                AverageRevenuePerOrder = averageRevenue,
                TopOrders = topOrders,
                MostPopularMeals = mostPopular,
                LeastPopularMeals = leastPopular
            };

            await _reportsRepository.SaveReportAsync(report);

            return report;
        }

        public async Task<byte[]> GenerateAndSaveReportPdfAsync(Guid restaurantId, int daysBack = 30)
        {
            var report = await GenerateAndSaveMonthlyReportAsync(restaurantId, daysBack);
            var pdfBytes = _pdfService.GenerateReportPdf(report);
            return pdfBytes;
        }

        public async Task<MonthlyReport?> GetLatestSavedReportAsync(Guid restaurantId)
        {
            return await _reportsRepository.GetReportAsync(restaurantId);
        }
    }
}
