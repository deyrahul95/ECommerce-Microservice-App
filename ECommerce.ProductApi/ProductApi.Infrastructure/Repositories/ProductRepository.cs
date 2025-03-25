using ECommerce.Shared.Repositories;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.DB;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
{

}
