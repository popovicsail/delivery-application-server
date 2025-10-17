using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register-courier")]
    public async Task<IActionResult> RegisterCourier([FromBody] CourierCreateRequestDto request)
    {
        await _adminService.RegisterCourierAsync(request);
        return Ok();
    }

    [HttpPost("register-owner")]
    public async Task<IActionResult> RegisterOwner([FromBody] OwnerCreateRequestDto request)
    {
        await _adminService.RegisterOwnerAsync(request);
        return Ok();
    }

    [HttpDelete("delete-user/{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _adminService.DeleteUserAsync(userId);
        return Ok();
    }
}
