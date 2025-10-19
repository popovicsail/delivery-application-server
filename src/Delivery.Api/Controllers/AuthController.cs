using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;
using Delivery.Application.Interfaces;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
    {
        RegisterResponseDto registerResponse = await _authService.RegisterAsync(registerRequest);

        return Ok(registerResponse);
    }

    [HttpPost("logout")]
    public IActionResult LogoutAsync()
    {
        return Ok();
    }
}
