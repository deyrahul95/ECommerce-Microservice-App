using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.DB;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository(
    ProductDbContext dbContext,
    LoggerService logger) : BaseRepository<Product>(dbContext, logger), IProductRepository
{

}
