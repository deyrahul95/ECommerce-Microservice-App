using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.DB;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository(
    ProductDbContext dbContext,
    ILoggerService logger) : BaseRepository<Product>(dbContext, logger), IProductRepository
{

}
