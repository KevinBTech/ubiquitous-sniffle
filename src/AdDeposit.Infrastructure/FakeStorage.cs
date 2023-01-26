using AdDeposit.Core;

namespace AdDeposit.Infrastructure
{
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