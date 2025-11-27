using Delivery.Domain.Entities.DishEntities;

namespace Delivery.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetOneAsync(Guid id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Dispose();
}
