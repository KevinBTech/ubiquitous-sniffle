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

        public AdsController(AdsCreation adsCreation)
        {
            _adsCreation = adsCreation;
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