using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var restaurants = await _restaurantService.GetAllAsync();

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDetailResponseDto>> GetOneAsync([FromRoute] Guid id)
    {
        var restaurant = await _restaurantService.GetOneAsync(id);

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantDetailResponseDto>> CreateAsync(RestaurantCreateRequestDto request)
    {
        var restaurant = await _restaurantService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, RestaurantUpdateRequestDto request)
    {
        await _restaurantService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _restaurantService.DeleteAsync(id);

        return NoContent();
    }
}
