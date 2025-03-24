using System.Linq.Expressions;

namespace ECommerce.Shared.Repository.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public Task CreateAsync(T entity, CancellationToken token = default);
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
    public Task<T?> FindByIdAsync(Guid id, CancellationToken token = default);
    public Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    public Task UpdateASync(T entity, CancellationToken token = default);
    public Task DeleteAsync(T entity, CancellationToken token = default);
}
