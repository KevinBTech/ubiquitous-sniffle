using AdDeposit.Core;
using AdDeposit.Domain.Ads.Wheather;
using AdDeposit.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdDeposit.WebApi.Controllers
{
    [ApiController]
    [Route("api/add/{adId}/wheather")]
    public class WeatherAdController : ControllerBase
    {
        private readonly IQueryHandler<GetAdWheather, AdWheater> _getWheather;

        public WeatherAdController(IQueryHandler<GetAdWheather, AdWheater> getWheather)
        {
            _getWheather = getWheather;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long adId)
        {
            var adWheater = await _getWheather.HandleAsync(new GetAdWheather(adId));

            if (adWheater == null)
            {
                return NoContent();
            }

            return Ok(adWheater);
        }

        public sealed class AdToAdd
        {
            [Required]
            public string Title { get; set; } = null!;

            [Required]
            public string Description { get; set; } = null!;

            [Required]
            public Localization Localization { get; set; } = null!;
        }
    }
}