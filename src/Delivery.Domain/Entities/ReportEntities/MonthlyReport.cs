using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.ReportEntities
{
    public class MonthlyReport
    {
        public string Id { get; set; }

        public Guid RestaurantId { get; set; }

        public DateTime GeneratedAt { get; set; }

        public double TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public double AverageRevenuePerOrder { get; set; }

        public virtual List<OrderSummary> TopOrders { get; set; }
        public virtual List<MealCount> MostPopularMeals { get; set; }
        public virtual List<MealCount> LeastPopularMeals { get; set; }
    }
    public class OrderSummary
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }
        public virtual List<OrderMealItem> Items { get; set; }
    }

    public class OrderMealItem
    {
        public Guid DishId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }

    public class MealCount
    {
        public Guid DishId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
