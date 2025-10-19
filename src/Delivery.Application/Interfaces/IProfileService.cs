using System.Security.Claims;
using Delivery.Application.Dtos.Users.ProfileDtos.Requests;
using Delivery.Application.Dtos.Users.ProfileDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IProfileService
{
    Task<ProfileResponseDto> GetOneAsync(Guid id);
    Task<ProfileResponseDto> UpdateAsync(ClaimsPrincipal principal, ProfileUpdateRequestDto request);
}
