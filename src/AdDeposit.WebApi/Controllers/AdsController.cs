using AdDeposit.Core;
using AdDeposit.Domain.Ads;
using AdDeposit.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdDeposit.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly AdsCreation _adsCreation;
        private readonly IReadRepository<Ad> _adsQuery;

        public AdsController(
            AdsCreation adsCreation,
            IReadRepository<Ad> adsQuery
            )
        {
            _adsCreation = adsCreation;
            _adsQuery = adsQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
                    [Required] AdToAdd adToCreate)
        {
            try
            {
                var createdAd = await _adsCreation.ExecuteAsync(
                    new AdsToCreate(
                        adToCreate.Title,
                        adToCreate.Description,
                        adToCreate.Localization,
                        adToCreate.AdType
                        )
                    );

                return Ok(createdAd.Id);
            }
            catch (InvalidOperationException ex)
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(long adId)
        {
            var existingAd = await _adsQuery.GetAsync(adId);

            if (existingAd != null &&
                existingAd.CurrentState == AdState.Published)
            {
                return Ok(existingAd);
            }

            return NotFound();
        }

        public sealed class AdToAdd
        {
            [Required]
            public string Title { get; set; } = null!;

            [Required]
            public string Description { get; set; } = null!;

            [Required]
            public Localization Localization { get; set; } = null!;

            [Required]
            public string AdType { get; set; } = null!;
        }
    }
}