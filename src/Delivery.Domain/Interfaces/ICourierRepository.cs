using Delivery.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Interfaces;

public interface ICourierRepository : IGenericRepository<Courier>
{
    Task<Courier?> GetOneWithUserAsync(Guid id);

    Task<List<Courier>> GetAllWithSchedulesAsync();

    Task<IEnumerable<Courier>> GetAllWithOrdersAsync();



}
