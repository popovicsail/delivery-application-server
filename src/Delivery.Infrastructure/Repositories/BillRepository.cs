using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Mappings.MongoMappings.OrderMappings;
using MongoDB.Driver;
using Delivery.Infrastructure.Persistence.MongoEntities.OrderEntities;

namespace Delivery.Infrastructure.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly IMongoCollection<BillDocument> _bills;

        public BillRepository(IMongoDatabase database)
        {
            _bills = database.GetCollection<BillDocument>("bills");
        }

        public async Task CreateAsync(Bill bill)
        {
            var document = bill.ToDocument();

            await _bills.InsertOneAsync(document);

            bill.Id = document.Id;
        }

        public async Task<Bill?> GetByOrderIdAsync(Guid orderId)
        {
            var document = await _bills
                .Find(b => b.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (document == null)
                return null;

            return document.ToEntity();
        }
    }
}