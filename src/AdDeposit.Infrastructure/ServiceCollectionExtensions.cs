using AdDeposit.Core;
using AdDeposit.Domain.Ads.Wheather;
using AdDeposit.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdDeposit.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IWriteRepository<>), typeof(FakeStorage<>));
            services.TryAddSingleton(typeof(IReadRepository<>), typeof(FakeStorage<>));

            return services;
        }

        public static IServiceCollection AddAdWheather(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IQueryHandler<GetAdWheather, AdWheater>), typeof(AdWheaterRepository));

            services.TryAddScoped(typeof(IQueryHandler<GetAdWheather, AdWheater>), typeof(AdWheaterRepository));

            services.AddHttpClient<GeocodingOpenMeteoApi>();
            services.AddHttpClient<WheaterForecastOpenMeteoApi>();

            return services;
        }
    }
}