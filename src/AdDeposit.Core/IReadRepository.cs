using System.Linq.Expressions;

namespace AdDeposit.Core
{
    public interface IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity?> GetAsync(long id);
    }
}