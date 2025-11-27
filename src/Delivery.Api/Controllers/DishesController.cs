using Delivery.Application.Dtos.CommonDtos.AllergenDtos;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Domain.Entities.DishEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] DishFiltersMix filters, string sort, int page = 1)
    {
        var restaurants = await _dishService.GetPagedAsync(sort, filters, page, User);

        return Ok(restaurants);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetAllFilteredAsync([FromQuery] DishFiltersMix filters, string sort)
    {
        var dishes = await _dishService.GetAllFilteredAsync(filters, sort);

        return Ok(dishes);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var dishes = await _dishService.GetAllAsync();

        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DishDetailResponseDto>> GetOneAsync([FromRoute] Guid id)
    {
        var dish = await _dishService.GetOneAsync(id);

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<DishDetailResponseDto>> CreateAsync([FromForm] DishCreateRequestDto request, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!string.IsNullOrEmpty(Request.Form["AllergenIds"]))
        {
            request.AllergenIds = JsonConvert.DeserializeObject<List<Guid>>(Request.Form["AllergenIds"]);
        }
        var dish = await _dishService.AddAsync(request, file);

        return CreatedAtAction("GetOne", new { id = dish.Id }, dish);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] DishUpdateRequestDto request, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!string.IsNullOrEmpty(Request.Form["AllergenIds"]))
        {
            request.AllergenIds = JsonConvert.DeserializeObject<List<Guid>>(Request.Form["AllergenIds"]);
        }
        await _dishService.UpdateAsync(id, request, file);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _dishService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("menu/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<DishDetailResponseDto>> GetMenuAsync([FromRoute] Guid id)
    {
        var menu = await _dishService.GetMenuAsync(id);

        return Ok(menu);
    }
}
