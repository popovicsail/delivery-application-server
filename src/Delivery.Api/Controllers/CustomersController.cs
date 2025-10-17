using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();

        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDetailResponseDto>> GetOneAsync(Guid id)
    {
        var customer = await _customerService.GetOneAsync(id);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDetailResponseDto>> CreateAsync([FromBody] CustomerCreateRequestDto request)
    {
        var customer = await _customerService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = customer.Id }, customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CustomerUpdateRequestDto request)
    {
        await _customerService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _customerService.DeleteAsync(id);

        return NoContent();
    }
}
