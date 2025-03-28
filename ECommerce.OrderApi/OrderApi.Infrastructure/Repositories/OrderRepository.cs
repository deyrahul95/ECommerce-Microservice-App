using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.DB;
using OrderApi.Infrastructure.Repositories.Interfaces;

namespace OrderApi.Infrastructure.Repositories;

public class OrderRepository(
    OrderDbContext dbContext,
    ILoggerService logger) : BaseRepository<Order>(dbContext, logger), IOrderRepository
{
    private readonly OrderDbContext _dbContext = dbContext;
    private readonly ILoggerService _logger = logger;

    public async Task<List<Order>> GetClientOrders(Guid clientId, CancellationToken token = default)
    {
        try
        {
            return await _dbContext
                .Orders
                .AsNoTracking()
                .Where(o => o.ClientId == clientId)
                .ToListAsync(token);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to fetched orders. User Id: {clientId}", ex);
            return [];
        }
    }
}
