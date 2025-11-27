using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;

    public OwnersController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var owners = await _ownerService.GetAllAsync();

        return Ok(owners);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OwnerDetailResponseDto>> GetOneAsync(Guid id)
    {
        var owner = await _ownerService.GetOneAsync(id);

        return Ok(owner);
    }

    [HttpPost]
    public async Task<ActionResult<OwnerDetailResponseDto>> CreateAsync([FromBody] OwnerCreateRequestDto request)
    {
        var owner = await _ownerService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = owner.Id }, owner);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] OwnerUpdateRequestDto request)
    {
        await _ownerService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _ownerService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("permit/restaurant/{restaurantId:guid}")]
    public async Task<IActionResult> GetMenuPermissionAsync(Guid userId, Guid restaurantId)
    {
        var permit = await _ownerService.GetRestaurantPermissionAsync(User!, restaurantId);
        return Ok(permit);
    }
}
