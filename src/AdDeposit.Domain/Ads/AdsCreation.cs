using AdDeposit.Core;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Domain.Ads
{
    public sealed class AdsCreation
    {
        private readonly IWriteRepository<Ad> _adWriteRepository;

        public AdsCreation(IWriteRepository<Ad> adWriteRepository)
        {
            _adWriteRepository = adWriteRepository;
        }

        public async Task<Ad> ExecuteAsync(AdsToCreate adsToCreate)
        {
            var newAd = new Ad(
                adsToCreate.Title,
                adsToCreate.Description,
                adsToCreate.Localization);

            await _adWriteRepository.AddAsync(newAd);

            return newAd;
        }
    }

    public sealed record AdsToCreate(
        string Title,
        string Description,
        Localization Localization
        )
    { }
}