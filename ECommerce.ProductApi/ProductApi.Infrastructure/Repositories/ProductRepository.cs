using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.DB;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository(
    ProductDbContext dbContext,
    ILoggerService logger) : BaseRepository<Product>(dbContext, logger), IProductRepository
{
    private readonly ProductDbContext _dbContext = dbContext;
    private readonly ILoggerService _logger = logger;

    public async Task<Product?> GetByName(string name, CancellationToken token = default)
    {
        try
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Name.ToLower() == name.ToLower(),
                    token);

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to fetched product. Name: {name}", ex);
            return null;
        }
    }
}
