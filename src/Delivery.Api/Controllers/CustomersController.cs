using System.Security.Claims;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
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




    // -----------------------------
    // Moje adrese
    // -----------------------------

    [HttpGet("my-addresses")]
    public async Task<IActionResult> GetMyAddresses()
    {
        var addresses = await _customerService.GetMyAddressesAsync(User);
        return Ok(addresses);
    }

    [HttpPost("my-addresses")]
    public async Task<IActionResult> CreateAddress([FromQuery] double latitude, [FromQuery] double longitude)
    {
        await _customerService.CreateAddressAsync(User, latitude, longitude);
        return Ok();
    }

    [HttpPut("my-addresses/{addressId:guid}")]
    public async Task<IActionResult> UpdateAddress(Guid addressId, [FromBody] AddressUpdateRequest request)
    {
        await _customerService.UpdateAddressAsync(User, addressId, request);
        return NoContent();
    }

    [HttpDelete("my-addresses/{addressId:guid}")]
    public async Task<IActionResult> DeleteAddress(Guid addressId)
    {
        await _customerService.DeleteAddressAsync(User, addressId);
        return NoContent();
    }

    // -----------------------------
    // Moji alergeni
    // -----------------------------

    [HttpGet("my-allergens")]
    public async Task<IActionResult> GetMyAllergens()
    {
        var allergenIds = await _customerService.GetMyAllergensAsync(User);
        return Ok(allergenIds);
    }

    [HttpPut("my-allergens")]
    public async Task<IActionResult> UpdateMyAllergens([FromBody] UpdateCustomerAllergensRequest request)
    {
        await _customerService.UpdateMyAllergensAsync(User, request);
        return NoContent();

    }



    [HttpGet("my-vouchers")]
    public async Task<IActionResult> GetMyVouchers()
    {
        var Vouchers = await _customerService.GetMyVouchersAsync(User);

        return Ok(Vouchers);
    }
}
