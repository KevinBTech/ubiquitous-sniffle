using AdDeposit.Core;

namespace AdDeposit.Tests.Ads
{
    public sealed class FakeRepository<TEntity> :
        IWriteRepository<TEntity>,
        IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly List<TEntity> _entities = new();

        public Task AddAsync(TEntity entity)
        {
            entity.Id = _entities.Count + 1;
            _entities.Add(entity);

            return Task.CompletedTask;
        }

        public Task<TEntity?> GetAsync(long id)
        {
            return Task.FromResult(_entities.FirstOrDefault(e => e.Id == id));
        }

        public Task UpdateAsync(TEntity entity)
        {
            int entityIndex = _entities.FindIndex(e => e.Id == entity.Id);

            if (entityIndex == -1)
            {
                _entities[entityIndex] = entity;
            }

            return Task.CompletedTask;
        }
    }
}