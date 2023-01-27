using AdDeposit.Domain.Ads;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Tests.Ads
{
    public class AdsCreationTest
    {
        [Fact]
        public async Task ExecuteAsync_Must_CreateANewAd()
        {
            var adsCreation = new AdsCreation(new FakeRepository<Ad>());

            var createdAd = await adsCreation.ExecuteAsync(
                new AdsToCreate(
                    "Appartement 3 pièces 75m2",
                    "1 SDB, 2 CH (15m2 et 10m2), 1 SAL, 1 CUI",
                    new Localization("5 rue de truc", "00000", "STRASBOURG", "FRANCE"),
                    "flat")
                );

            Assert.NotNull(createdAd);
            Assert.Equal(1, createdAd.Id);
        }

        [Fact]
        public async Task ExecuteAsync_Must_HaveANewAdWith_State_Equal_To_WaitingForValidation()
        {
            var adsCreation = new AdsCreation(new FakeRepository<Ad>());

            var createdAd = await adsCreation.ExecuteAsync(
                new AdsToCreate(
                    "Appartement 3 pièces 75m2",
                    "1 SDB, 2 CH (15m2 et 10m2), 1 SAL, 1 CUI",
                    new Localization("5 rue de truc", "00000", "STRASBOURG", "FRANCE"),
                    "house")
                );

            Assert.NotNull(createdAd);
            Assert.Equal(AdState.WaitingForValidation, createdAd.CurrentState);
        }

        [Fact]
        public async Task ExecuteAsync_WithUnexpectedAdType_Must_Fail()
        {
            var adsCreation = new AdsCreation(new FakeRepository<Ad>());

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await adsCreation.ExecuteAsync(
                new AdsToCreate(
                    "Agricultural land",
                    "1000 m2",
                    new Localization("10 rue de truc", "00000", "STRASBOURG", "FRANCE"),
                    "land")
                ));
        }
    }
}