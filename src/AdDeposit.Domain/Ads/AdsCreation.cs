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
            if (!Enum.TryParse(adsToCreate.AdType, true, out AdType type))
            {
                throw new InvalidOperationException($"The ad type is not recognized among : " +
                    $"'{string.Join(",", Enum.GetNames<AdType>())}'");
            }

            var newAd = new Ad(
                adsToCreate.Title,
                adsToCreate.Description,
                adsToCreate.Localization,
                type
                );

            await _adWriteRepository.AddAsync(newAd);

            return newAd;
        }
    }

    public sealed record AdsToCreate(
        string Title,
        string Description,
        Localization Localization,
        string AdType
        )
    { }
}