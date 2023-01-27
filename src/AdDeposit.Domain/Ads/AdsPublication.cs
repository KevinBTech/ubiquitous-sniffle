using AdDeposit.Core;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Domain.Ads
{
    public sealed class AdsPublication
    {
        private readonly IReadRepository<Ad> _adReadRepository;
        private readonly IWriteRepository<Ad> _adWriteRepository;

        public AdsPublication(IReadRepository<Ad> adReadRepository, IWriteRepository<Ad> adWriteRepository)
        {
            _adReadRepository = adReadRepository;
            _adWriteRepository = adWriteRepository;
        }

        public async Task<Ad> ExecuteAsync(AdToPublish adToPublish)
        {
            var existingAdToPublish = await _adReadRepository.GetAsync(adToPublish.Id);

            if (existingAdToPublish == null)
            {
                throw new InvalidOperationException("The ad could not be published.");
            }

            existingAdToPublish.UpdateState(AdState.Published);

            await _adWriteRepository.UpdateAsync(existingAdToPublish);

            return existingAdToPublish;
        }
    }

    public sealed record AdToPublish(
        long Id
        )
    { }
}