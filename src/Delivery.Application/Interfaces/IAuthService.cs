using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
}
