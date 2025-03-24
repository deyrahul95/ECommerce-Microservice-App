using System.Linq.Expressions;
using ECommerce.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Repository;

public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> dbSet = context.Set<T>();

    public async Task CreateAsync(T entity, CancellationToken token = default)
    {
        await dbSet.AddAsync(entity, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
    {
        return await dbSet.ToListAsync(token);
    }

    public async Task<T?> FindByIdAsync(Guid id, CancellationToken token = default)
    {
        return await dbSet.FindAsync([id], cancellationToken: token);
    }

    public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
        return await dbSet.FindAsync([predicate], cancellationToken: token);
    }

    public async Task UpdateASync(T entity, CancellationToken token = default)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(T entity, CancellationToken token = default)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync(token);
    }
}
