using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IWorkerService
{
    Task<IEnumerable<WorkerSummaryResponseDto>> GetAllAsync();
    Task<WorkerDetailResponseDto?> GetOneAsync(Guid id);
    Task<WorkerDetailResponseDto> AddAsync(WorkerCreateRequestDto request);
    Task UpdateAsync(Guid id, WorkerUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
