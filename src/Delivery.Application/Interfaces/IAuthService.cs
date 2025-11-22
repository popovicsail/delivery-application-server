using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task RegisterAsync(RegisterRequestDto registerRequest);
    Task<LoginResponseDto> GoogleLoginAsync(GoogleLoginRequestDto googleRequest);
    Task ActivateAccountAsync(ActivateAccountRequestDto activateRequest);
    Task ForgotPasswordAsync(ForgotPasswordRequestDto forgotRequest);
    Task ResetPasswordAsync(ResetPasswordRequestDto resetRequest);
}
