using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<ActionResult<SingleCurrencyExchangeRateResponseDto>> GetSingleCurrencyExchangeRate([FromQuery] string baseCode)
        {
            var response = await _exchangeRateService.GetCurrencyExchangeRateAsync(baseCode);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}