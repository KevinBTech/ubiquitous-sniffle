using AdDeposit.Core;
using AdDeposit.Domain.Ads;
using AdDeposit.Domain.Entities;

namespace AdDeposit.Tests.Ads
{
    public class AdsCreationTest
    {
        [Fact]
        public async Task ExecuteAsync_Must_CreateANewAd()
        {
            var adsCreation = new AdsCreation(new FakeStorage<Ad>());

            var createdAd = await adsCreation.ExecuteAsync(
                new AdsToCreate(
                    "Appartement 3 pièces 75m2",
                    "1 SDB, 2 CH (15m2 et 10m2), 1 SAL, 1 CUI",
                    new Localization("5 rue de truc", "00000", "STRASBOURG", "FRANCE"))
                );

            Assert.NotNull(createdAd);
            Assert.Equal(1, createdAd.Id);
        }
    }

    public sealed class FakeStorage<TEntity> : IWriteRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly List<TEntity> _entities = new();

        public Task AddAsync(TEntity entity)
        {
            entity.Id = _entities.Count + 1;
            _entities.Add(entity);

            return Task.CompletedTask;
        }
    }
}