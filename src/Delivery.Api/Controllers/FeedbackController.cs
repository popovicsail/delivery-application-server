using Delivery.Application.Dtos.FeedbackDtos;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Delivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // samo prijavljeni korisnici mogu slati feedback
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    // GET: api/feedback/questions
    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var questions = await _feedbackService.GetAllQuestionsAsync();
        return Ok(questions);
    }

    // GET: api/feedback/user
    [HttpGet("user")]
    public async Task<IActionResult> GetUserFeedback()
    {
        var userId = GetCurrentUserId();
        var feedback = await _feedbackService.GetUserFeedbackAsync(userId);
        return Ok(feedback);
    }

    // POST: api/feedback
    // Kreira ili ažurira feedback korisnika
    [HttpPost]
    public async Task<IActionResult> SubmitFeedback([FromBody] IEnumerable<FeedbackCreateRequestDto> feedbackRequestDto)
    {
        var userId = GetCurrentUserId();
        await _feedbackService.SubmitFeedbackAsync(userId, feedbackRequestDto);
        return Ok(new { Message = "Feedback submitted successfully." });
    }

    // GET: api/feedback/statistics
    // Samo admin ili odgovarajuća uloga može pozvati
    [HttpGet("statistics")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetStatistics()
    {
        var stats = await _feedbackService.GetStatisticsAsync();
        return Ok(stats);
    }

    // 💡 Helper metoda za trenutno ulogovanog korisnika
    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) throw new UnauthorizedAccessException("User ID not found in token.");
        return Guid.Parse(userIdClaim);
    }
}
