using Delivery.Application.Dtos.CommonDtos.WorkScheduleDto;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Interfaces;

public interface ICourierService
{
    Task<IEnumerable<CourierSummaryResponseDto>> GetAllAsync();
    Task<CourierDetailResponseDto?> GetOneAsync(Guid id);
    Task<CourierDetailResponseDto> AddAsync(CourierCreateRequestDto request);
    Task UpdateAsync(Guid id, CourierUpdateRequestDto request);
    Task DeleteAsync(Guid id);
    Task UpdateAllCouriersStatusAsync();
    Task UpdateWorkSchedulesAsync(Guid courierId, CourierWorkSchedulesUpdateRequestDto request);
    Task<CourierStatusResponseDto> GetCourierStatusAsync(Guid courierId);

    Task<IEnumerable<WorkScheduleDto>> GetMyWorkSchedulesAsync(Guid courierId);
}
