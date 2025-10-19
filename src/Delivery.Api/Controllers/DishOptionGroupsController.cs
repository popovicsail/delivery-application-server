using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DishOptionGroupsController : ControllerBase
{
    private readonly IDishOptionGroupService _service;

    public DishOptionGroupsController(IDishOptionGroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishOptionGroupResponseDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DishOptionGroupResponseDto>> GetOne(Guid id)
    {
        return Ok(await _service.GetOneAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<DishOptionGroupResponseDto>> Create(DishOptionGroupCreateRequestDto request)
    {
        var group = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetOne), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DishOptionGroupResponseDto>> Update(Guid id, DishOptionGroupUpdateRequestDto request)
    {
        var updated = await _service.UpdateAsync(id, request);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
