using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.DB;
using OrderApi.Infrastructure.Repositories.Interfaces;

namespace OrderApi.Infrastructure.Repositories;

public class OrderRepository(
    OrderDbContext dbContext,
    ILoggerService logger) : BaseRepository<Order>(dbContext, logger), IOrderRepository
{

}
