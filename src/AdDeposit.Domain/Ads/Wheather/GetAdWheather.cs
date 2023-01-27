using AdDeposit.Core;

namespace AdDeposit.Domain.Ads.Wheather
{
    public sealed record GetAdWheather(
        long AdId
        )
        : IQuery<AdWheater>
    { }
}