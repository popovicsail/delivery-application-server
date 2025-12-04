using Delivery.Domain.Entities.ReportEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence.MongoEntities.ReportEntities;
using Delivery.Infrastructure.Persistence.MongoEntities.ReportEntities.Mappers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Delivery.Infrastructure.Persistence.MongoRepositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IMongoCollection<MonthlyReportDocument> _collection;

        public ReportsRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<MonthlyReportDocument>("MonthlyReports");
        }

        public async Task SaveReportAsync(MonthlyReport report)
        {
            var doc = MonthlyReportMapper.ToDocument(report);

            if (string.IsNullOrEmpty(doc.Id))
            {
                doc.Id = ObjectId.GenerateNewId().ToString();
                await _collection.InsertOneAsync(doc);
            }
            else
            {
                await _collection.ReplaceOneAsync(
                    x => x.Id == doc.Id,
                    doc,
                    new ReplaceOptions { IsUpsert = true }
                );
            }
        }

        public async Task<MonthlyReport?> GetReportAsync(Guid restaurantId)
        {
            var doc = await _collection
                .Find(x => x.RestaurantId == restaurantId)
                .SortByDescending(x => x.GeneratedAt)
                .FirstOrDefaultAsync();

            return MonthlyReportMapper.ToDomain(doc);
        }
    }
}
