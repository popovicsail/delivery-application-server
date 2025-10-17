using Delivery.Application.Dtos.Users.UserDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserSummaryResponseDto>> GetAllAsync();
}
