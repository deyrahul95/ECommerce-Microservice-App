using System.Linq.Expressions;
using ECommerce.Shared.Repositories.Interfaces;
using ECommerce.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Repositories;

public class BaseRepository<T>(DbContext context, LoggerService logger) : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> dbSet = context.Set<T>();

    public async Task<T?> CreateAsync(T entity, CancellationToken token = default)
    {
        try
        {
            var updatedEntity = await dbSet.AddAsync(entity, token);
            var result = await context.SaveChangesAsync(token);

            return result > 0 ? updatedEntity.Entity : null;
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to create entity.", ex);
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
    {
        try
        {
            return await dbSet.AsNoTracking().ToListAsync(token);
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to retried entities.", ex);
            return [];
        }
    }

    public async Task<T?> FindByIdAsync(Guid id, CancellationToken token = default)
    {
        try
        {
            return await dbSet.FindAsync([id], cancellationToken: token);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to retried entity, Id: {id}.", ex);
            return null;
        }
    }

    public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
        try
        {
            return await dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync(token);
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to retried entity.", ex);
            return null;
        }
    }

    public async Task<T?> UpdateASync(T entity, CancellationToken token = default)
    {
        try
        {
            var updatedEntity = dbSet.Update(entity);
            var result = await context.SaveChangesAsync(token);

            return result > 0 ? updatedEntity.Entity : null;
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to update entity.", ex);
            return null;
        }
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken token = default)
    {
        try
        {
            dbSet.Remove(entity);
            return await context.SaveChangesAsync(token) > 0;
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to delete entity.", ex);
            return false;
        }
    }
}
