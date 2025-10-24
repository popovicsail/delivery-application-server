using System.Security.Claims;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Courier")]
public class CouriersController : ControllerBase
{
    private readonly ICourierService _courierService;

    public CouriersController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var couriers = await _courierService.GetAllAsync();

        return Ok(couriers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourierDetailResponseDto>> GetOneAsync(Guid id)
    {
        var courier = await _courierService.GetOneAsync(id);

        return Ok(courier);
    }

    [HttpPost("debug/update-couriers-status")]
    public async Task<IActionResult> DebugUpdateCouriersStatus()
    {
        await _courierService.UpdateAllCouriersStatusAsync();
        return Ok("Statuses updated");
    }


    [HttpPost]
    public async Task<ActionResult<CourierDetailResponseDto>> CreateAsync([FromBody] CourierCreateRequestDto request)
    {
        var courier = await _courierService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = courier.Id }, courier);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CourierUpdateRequestDto request)
    {
        await _courierService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _courierService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("work-schedules")]
    public async Task<IActionResult> GetMyWorkSchedules()
    {
        var courierId = GetCourierIdFromClaims();
        var schedules = await _courierService.GetMyWorkSchedulesAsync(courierId);
        return Ok(schedules);
    }

    [HttpPut("work-schedules")]
    public async Task<IActionResult> UpdateWorkSchedules([FromBody] CourierWorkSchedulesUpdateRequestDto request)
    {
        var courierId = GetCourierIdFromClaims();
        await _courierService.UpdateWorkSchedulesAsync(courierId, request);
        return NoContent();
    }


    [HttpGet("{id}/status")]
    public async Task<IActionResult> GetCourierStatus(Guid id)
    {
        var statusDto = await _courierService.GetCourierStatusAsync(id);
        return Ok(statusDto);
    }


    private Guid GetCourierIdFromClaims()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                    ?? User.FindFirst("nameid"); // fallback ako ClaimTypes.NameIdentifier ne radi
        if (claim == null)
            throw new UnauthorizedAccessException("Courier ID claim not found.");
        return Guid.Parse(claim.Value);
    }

}
