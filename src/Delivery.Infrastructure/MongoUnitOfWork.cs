using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Repositories;
using MongoDB.Driver;

namespace Delivery.Infrastructure
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private IBillRepository _bills;

        public MongoUnitOfWork(IMongoDatabase database)
        {
            _database = database;
        }

        public IBillRepository Bills
        {
            get
            {
                return _bills ??= new BillRepository(_database);
            }
        }
    }
}