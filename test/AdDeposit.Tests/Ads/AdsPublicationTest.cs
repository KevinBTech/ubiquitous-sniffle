using AdDeposit.Domain.Ads;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Tests.Ads
{
    public class AdsPublicationTest
    {
        [Fact]
        public async Task ExecuteAsync_WithExistingAd_MustPublishIt()
        {
            var fake = new FakeRepository<Ad>();
            await fake.AddAsync(new Ad(
                    "Appartement 3 pièces 75m2",
                    "1 SDB, 2 CH (15m2 et 10m2), 1 SAL, 1 CUI",
                    new Localization("5 rue de truc", "00000", "STRASBOURG", "FRANCE"),
                    AdType.Parking)
                );
            var adsPublication = new AdsPublication(fake, fake);

            var ad = await adsPublication.ExecuteAsync(new AdToPublish(1));

            Assert.NotNull(ad);
            Assert.Equal(AdState.Published, ad.CurrentState);
        }

        [Fact]
        public async Task ExecuteAsync_MustNot_Publish_NotExistingAd()
        {
            var fake = new FakeRepository<Ad>();
            var adsPublication = new AdsPublication(fake, fake);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await adsPublication.ExecuteAsync(new AdToPublish(1)));
        }
    }
}