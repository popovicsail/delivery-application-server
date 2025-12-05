using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Delivery.Infrastructure.Persistence.MongoEntities.OrderEntities;

public class BillDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public Guid OrderId { get; set; }

    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }

    public DateTime IssuedAt { get; set; }
    public decimal TotalAmount { get; set; }

    public List<BillItemDocument> Items { get; set; } = new List<BillItemDocument>();
}

public class BillItemDocument
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}