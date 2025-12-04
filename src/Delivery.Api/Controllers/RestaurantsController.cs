using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.RestaurantEntities;
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

    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPagedAsync([FromQuery] RestaurantFiltersMix filters, int sort, int page = 1)
    {
        var restaurants = await _restaurantService.GetPagedAsync(sort, filters, page);

        return Ok(restaurants);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<RestaurantDetailResponseDto>> CreateAsync(RestaurantCreateRequestDto request)
    {
        var restaurant = await _restaurantService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator, Owner")]
    public async Task<ActionResult> UpdateAsync([FromForm] RestaurantUpdateRequestDto updateRequest, IFormFile? file, [FromRoute] Guid id)
    {
        await _restaurantService.UpdateAsync(id, updateRequest, file);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Owner")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _restaurantService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("{id}/menu")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurantMenuAsync([FromRoute] Guid id)
    {
        var response = await _restaurantService.GetRestaurantMenuAsync(id);
        return Ok(response);
    }

    [HttpGet("{restaurantId}/workers")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> GetWorkersAsync(Guid restaurantId)
    {
        var workers = await _restaurantService.GetWorkersAsync(restaurantId);
        return Ok(workers);
    }

    [HttpPost("{restaurantId}/workers")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> RegisterWorkerAsync(Guid restaurantId, [FromBody] WorkerCreateRequestDto request)
    {
        var worker = await _restaurantService.RegisterWorkerAsync(restaurantId, request, User);
        return Ok();
    }

    [Authorize(Roles = "Owner")]
    [HttpGet("my-restaurants")]
    public async Task<IActionResult> GetMyRestaurantsAsync()
    {
        var restaurants = await _restaurantService.GetMyRestaurantsAsync(User);
        return Ok(restaurants);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{restaurantId}/suspend-restaurant")]
    public async Task<ActionResult<RestaurantChangeSuspendStatusResponseDto>> ChangeRestaurantSuspendStatusAsync(Guid restaurantId, [FromBody] RestaurantChangeSuspendStatusRequestDto request)
    {
        var response = await _restaurantService.ChangeRestaurantSuspendStatusAsync(restaurantId, request);

        return Ok(response);
    }
}
