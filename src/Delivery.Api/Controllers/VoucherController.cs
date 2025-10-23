using System.Security.Claims;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class VouchersController : ControllerBase
{
    private readonly IVoucherService _voucherService;

    public VouchersController(IVoucherService voucherService)
    {
        _voucherService = voucherService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<VoucherDetailResponseDto>> GetOneAsync([FromRoute] Guid id)
    {
        var voucher = await _voucherService.GetOneAsync(id);

        return Ok(voucher);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<VoucherDetailResponseDto>> CreateAsync(VoucherCreateRequestDto request)
    {
        var voucher = await _voucherService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = voucher.Id }, voucher);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator, Owner")]
    public async Task<ActionResult> UpdateAsync([FromForm] VoucherUpdateRequestDto updateRequest, [FromRoute] Guid id)
    {
        await _voucherService.UpdateAsync(id, updateRequest);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Owner")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _voucherService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost("{id}/use")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult> UseVoucherAsync(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            throw new NotFoundException("ERROR: User not found.");
        }

        await _voucherService.UseVoucherAsync(id, Guid.Parse(userId));

        return NoContent();
    }
}
