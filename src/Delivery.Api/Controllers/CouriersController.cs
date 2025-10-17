using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
}
