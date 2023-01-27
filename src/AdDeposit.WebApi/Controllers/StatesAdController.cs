using AdDeposit.Domain.Ads;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdDeposit.WebApi.Controllers
{
    [ApiController]
    [Route("api/ads/{adId}/states")]
    public class StatesAdController : ControllerBase
    {
        private readonly AdsPublication _adsPublication;
        private readonly ILogger _logger;

        public StatesAdController(AdsPublication adsPublication, ILogger<AdsPublication> logger)
        {
            _adsPublication = adsPublication;
            _logger = logger;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish([Required] long adId)
        {
            try
            {
                _ = await _adsPublication.ExecuteAsync(new AdToPublish(adId));

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                var message = ex.Message;
                _logger.LogError(ex, "");
                return BadRequest(message);
            }
        }
    }
}