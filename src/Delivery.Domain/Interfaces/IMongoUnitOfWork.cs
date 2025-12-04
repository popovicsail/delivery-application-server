namespace Delivery.Domain.Interfaces;

public interface IMongoUnitOfWork
{
    IBillRepository Bills { get; }
}
