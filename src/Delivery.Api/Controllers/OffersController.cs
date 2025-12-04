using Delivery.Application.Dtos.OfferDtos;
using Delivery.Application.Dtos.OfferDtos.Requests;
using Delivery.Application.Dtos.OfferDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public async Task<IActionResult> GetByRestaurantAsync([FromQuery] Guid restaurantId)
            {
            var offers = await _offerService.GetByRestaurantAsync(restaurantId, User);
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDetailsResponseDto>> GetOneAsync([FromRoute] Guid id)
        {
            var offer = await _offerService.GetOneAsync(id);
            return Ok(offer);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<ActionResult<OfferDetailsResponseDto>> CreateAsync([FromQuery] Guid restaurantId, [FromForm] OfferCreateRequestDto request, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var offer = await _offerService.AddAsync(restaurantId, request, file);
            return Ok(offer);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] OfferUpdateRequestDto request, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var offer = await _offerService.UpdateAsync(id, request, file);
            return Ok(offer);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("{offerId}/dishes/manage")]
        public async Task<ActionResult<OfferDetailsResponseDto>> ManageItemsInOfferAsync(Guid offerId, [FromBody] List<OfferDishDto> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _offerService.ManageItemsInOfferAsync(offerId, dtos);
            return NoContent();
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _offerService.DeleteAsync(id);
            return NoContent();
        }
    }
}