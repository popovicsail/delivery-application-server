using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AllergensController : ControllerBase
{
    private readonly IAllergenService _allergenService;

    public AllergensController(IAllergenService allergenService)
    {
        _allergenService = allergenService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var allergens = await _allergenService.GetAllAsync();

        return Ok(allergens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AllergenDetailResponseDto>> GetOneAsync([FromRoute] Guid id)
    {
        var allergen = await _allergenService.GetOneAsync(id);

        return Ok(allergen);
    }

    [HttpPost]
    public async Task<ActionResult<AllergenDetailResponseDto>> CreateAsync(AllergenCreateRequestDto request)
    {
        var allergen = await _allergenService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = allergen.Id }, allergen);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, AllergenUpdateRequestDto request)
    {
        await _allergenService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _allergenService.DeleteAsync(id);

        return NoContent();
    }
}
