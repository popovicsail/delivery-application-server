using Delivery.Domain.Common;

namespace Delivery.Domain.Interfaces
{
    public interface IAreasOfOperationRepository : IGenericRepository<AreaOfOperation>
    {
        Task<bool> GetAreaConditionsByCity(string city);
    }
}
