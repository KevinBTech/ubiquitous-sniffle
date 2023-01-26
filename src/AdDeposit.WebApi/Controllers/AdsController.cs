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
            var createdAd = await _adsCreation.ExecuteAsync(
                new AdsToCreate(
                    adToCreate.Title,
                    adToCreate.Description,
                    adToCreate.Localization)
                );

            if (createdAd != null)
            {
                return Ok(createdAd.Id);
            }
            else
            {
                return BadRequest("The ad has not been created.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(long adId)
        {
            var existingAd = await _adsQuery.GetAsync(adId);

            if (existingAd != null)
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
        }
    }
}