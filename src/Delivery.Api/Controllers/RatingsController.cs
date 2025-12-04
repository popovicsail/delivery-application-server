using Microsoft.AspNetCore.Mvc;
using Delivery.Application.Interfaces;
using Delivery.Application.Dtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // ➡️ Kreiranje nove ocene (kupac ocenjuje restoran ili kurira)
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromForm] CreateRatingRequestDto dto)
        {
            var ratingId = await _ratingService.CreateRatingAsync(dto, User);
            return Ok(new { ratingId });
        }

        // ➡️ Prikaz svih ocena za restoran sa paginacijom i filterima
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantRatings(
            Guid restaurantId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var (ratings, totalCount) = await _ratingService.GetRatingsForRestaurantAsync(
                restaurantId, page, pageSize, from, to);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                ratings
            });
        }

        // ➡️ Prikaz svih ocena za kurira sa paginacijom i filterima
        [HttpGet("courier/{courierId}")]
        public async Task<IActionResult> GetCourierRatings(
            Guid courierId,
            int page = 1,
            int pageSize = 10,
            DateTime? from = null,
            DateTime? to = null)
        {
            var (ratings, totalCount) = await _ratingService.GetRatingsForCourierAsync(
                courierId, page, pageSize, from, to);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                ratings
            });
        }

        [HttpGet("average/{targetType}/{targetId}")]
        public async Task<IActionResult> GetTargetAverageRating(int targetType, Guid targetId)
        {
            return Ok(await _ratingService.GetAverageRatingAsync(targetId, targetType));
        }

        // ➡️ Helper metoda za izvlačenje userId iz JWT tokena
        private Guid GetUserIdFromClaims()
        {
            return Guid.Parse(User.FindFirst("sub")?.Value
                ?? throw new Exception("Missing user ID in claims"));
        }
    }
}
