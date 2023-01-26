using AdDeposit.Core;

namespace AdDeposit.Infrastructure
{
    public sealed class FakeStorage<TEntity> :
        IWriteRepository<TEntity>,
        IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        private static readonly List<TEntity> _entities = new();

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
    }
}