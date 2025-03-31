using AuthApi.DB;
using AuthApi.Users.Entities;
using AuthApi.Users.Repositories.Interfaces;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;

namespace AuthApi.Users.Repositories;

public class UserRepository(
    UserDbContext dbContext,
    ILoggerService logger) : BaseRepository<AppUser>(dbContext, logger), IUserRepository
{
    
}
