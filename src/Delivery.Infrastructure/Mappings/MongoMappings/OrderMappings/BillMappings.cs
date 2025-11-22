using Delivery.Domain.Entities.OrderEntities;
using Delivery.Infrastructure.Persistence.MongoEntities.OrderEntities;

namespace Delivery.Infrastructure.Mappings.MongoMappings.OrderMappings
{
    public static class BillMappings
    {
        public static BillDocument ToDocument(this Bill entity)
        {
            return new BillDocument
            {
                Id = entity.Id,

                OrderId = entity.OrderId,

                CustomerName = entity.CustomerName,
                CustomerEmail = entity.CustomerEmail,
                IssuedAt = entity.IssuedAt,
                TotalAmount = entity.TotalAmount,

                Items = entity.Items.Select(item => new BillItemDocument
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }

        public static Bill ToEntity(this BillDocument doc)
        {
            return new Bill
            {
                Id = doc.Id,

                OrderId = doc.OrderId,

                CustomerName = doc.CustomerName,
                CustomerEmail = doc.CustomerEmail,
                IssuedAt = doc.IssuedAt,
                TotalAmount = doc.TotalAmount,

                Items = doc.Items.Select(item => new BillItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}
