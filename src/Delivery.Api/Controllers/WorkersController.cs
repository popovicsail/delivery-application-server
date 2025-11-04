using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;
using Delivery.Application.Interfaces;
using Delivery.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkersController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var workers = await _workerService.GetAllAsync();

        return Ok(workers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkerDetailResponseDto>> GetOneAsync(Guid id)
    {
        var worker = await _workerService.GetOneAsync(id);

        return Ok(worker);
    }

    [HttpPost]
    public async Task<ActionResult<WorkerDetailResponseDto>> CreateAsync([FromBody] WorkerCreateRequestDto request)
    {
        var worker = await _workerService.AddAsync(request);

        return CreatedAtAction("GetOne", new { id = worker.Id }, worker);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] WorkerUpdateRequestDto request)
    {
        await _workerService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpPut("{id:guid}/suspend")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> SuspendWorkerAsync(Guid id)
    {
        await _workerService.SuspendWorkerAsync(id);
        return Ok();
    }

    [HttpPut("{id:guid}/unsuspend")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UnsuspendWorkerAsync(Guid id)
    {
        await _workerService.UnsuspendWorkerAsync(id);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _workerService.DeleteAsync(id);

        return NoContent();
    }
}
