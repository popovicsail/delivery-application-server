using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Delivery.Infrastructure.Persistence.MongoEntities.ReportEntities
{
    public class MonthlyReportDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid RestaurantId { get; set; }

        public DateTime GeneratedAt { get; set; }

        public double TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public double AverageRevenuePerOrder { get; set; }

        public List<OrderSummaryDocument> TopOrders { get; set; }
        public List<MealCountDocument> MostPopularMeals { get; set; }
        public List<MealCountDocument> LeastPopularMeals { get; set; }
    }

    public class MealCountDocument
    {
        public Guid DishId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class OrderMealItemDocument
    {
        public Guid DishId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }

    public class OrderSummaryDocument
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderMealItemDocument> Items { get; set; }
    }
}
