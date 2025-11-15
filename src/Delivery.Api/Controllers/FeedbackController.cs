using Delivery.Application.Dtos.FeedbackDtos;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Delivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _feedbackService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserFeedback()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var responses = await _feedbackService.GetUserFeedbackAsync(userId);
            return Ok(responses);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitFeedback([FromBody] IEnumerable<FeedbackCreateRequestDto> feedbackDtos)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _feedbackService.SubmitFeedbackAsync(userId, feedbackDtos);
            return Ok(new { message = "Feedback successfully submitted." });
        }

        [HttpPost("statistics")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetStatistics([FromBody] FeedbackFilterRequestDto request)
        {
            var stats = await _feedbackService.GetFilteredResponsesAsync(request);
            return Ok(stats);
        }

    }
}
