using Delivery.Application.Dtos.AdressValidationDtos.Requests;
using Delivery.Application.Dtos.AdressValidationDtos.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressValidationService _addressValidationService;

        public AddressController(IAddressValidationService addressValidationService)
        {
            _addressValidationService = addressValidationService;
        }

        /// <summary>
        /// Validacija adrese putem Nominatim API-ja
        /// </summary>
        /// <param name="request">Adresa i grad restorana</param>
        /// <returns>Rezultat validacije (IsValid + Message)</returns>
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateAddress([FromBody] AddressValidationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Address) || string.IsNullOrWhiteSpace(request.RestaurantCity))
            {
                return BadRequest(new AddressValidationResultDto
                {
                    IsValid = false,
                    Message = "Adresa i grad restorana su obavezni."
                });
            }

            var result = await _addressValidationService.ValidateAsync(request.Address, request.RestaurantCity);
            return Ok(result);
        }
    }

}
