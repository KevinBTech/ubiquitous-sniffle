using AdDeposit.Core;
using AdDeposit.Domain.Ads.Wheather;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Infrastructure.Queries
{
    internal sealed class AdWheaterRepository : IQueryHandler<GetAdWheather, AdWheater>
    {
        private readonly IReadRepository<Ad> _adReadRepository;
        private readonly GeocodingOpenMeteoApi _geocodingOpenMeteoApi;
        private readonly WheaterForecastOpenMeteoApi _wheaterForecastOpenMeteoApi;

        public AdWheaterRepository(
            IReadRepository<Ad> adReadRepository,
            GeocodingOpenMeteoApi geocodingOpenMeteoApi,
            WheaterForecastOpenMeteoApi wheaterForecastOpenMeteoApi)
        {
            _adReadRepository = adReadRepository;
            _geocodingOpenMeteoApi = geocodingOpenMeteoApi;
            _wheaterForecastOpenMeteoApi = wheaterForecastOpenMeteoApi;
        }

        public async Task<AdWheater?> HandleAsync(GetAdWheather query)
        {
            var ad = await _adReadRepository.GetAsync(query.AdId);
            if (ad is not null)
            {
                var result = await _geocodingOpenMeteoApi.GetAsync(ad.Localization.City, ad.Localization.Country);

                if (result is not null)
                {
                    var adWheater = await _wheaterForecastOpenMeteoApi.GetAsync(result);
                    return adWheater;
                }
            }

            return null;
        }
    }
}