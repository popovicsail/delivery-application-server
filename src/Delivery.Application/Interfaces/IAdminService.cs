using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;

namespace Delivery.Application.Interfaces
{
    public interface IAdminService
    {
        Task RegisterCourierAsync(CourierCreateRequestDto request);
        Task RegisterOwnerAsync(OwnerCreateRequestDto request);
        Task DeleteUserAsync(Guid userId);
    }
}
