namespace AdDeposit.Core
{
    public interface IWriteRepository<in TEntity>
        where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
    }
}