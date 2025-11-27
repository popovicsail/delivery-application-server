using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
    {
        LoginResponseDto loginResponseDto = await _authService.LoginAsync(loginRequest);

        return Ok(loginResponseDto);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLoginAsync([FromBody] GoogleLoginRequestDto googleRequest)
    {
        LoginResponseDto loginResponse = await _authService.GoogleLoginAsync(googleRequest);

        return Ok(loginResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
    {
        await _authService.RegisterAsync(registerRequest);

        return Ok();
    }

    [HttpGet("activate-account")]
    [AllowAnonymous]
    public async Task<IActionResult> ActivateAccountAsync([FromQuery] ActivateAccountRequestDto activateRequest)
    {
        await _authService.ActivateAccountAsync(activateRequest);

    return Ok(new { Message = "Nalog je uspešno aktiviran." });
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequestDto forgotRequest)
    {
        await _authService.ForgotPasswordAsync(forgotRequest);

        return Ok(new { Message = "Please check your email in order to reset your password." });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequestDto resetRequest)
    {
        await _authService.ResetPasswordAsync(resetRequest);

        return Ok(new { Message = "Error changed successfully. You may now log in." });
    }


    [HttpPost("logout")]
    public IActionResult LogoutAsync()
    {
        return Ok();
    }
}
