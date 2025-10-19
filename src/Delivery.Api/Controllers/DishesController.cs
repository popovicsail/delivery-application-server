using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var dishs = await _dishService.GetAllAsync();

        return Ok(dishs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DishDetailResponseDto>> GetOneAsync([FromRoute] Guid id)
    {
        var dish = await _dishService.GetOneAsync(id);

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<DishDetailResponseDto>> CreateAsync(DishCreateRequestDto request)
    {
        var dish = await _dishService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = dish.Id }, dish);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, DishUpdateRequestDto request)
    {
        await _dishService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _dishService.DeleteAsync(id);

        return NoContent();
    }
}
