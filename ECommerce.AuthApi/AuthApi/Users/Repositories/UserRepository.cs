using AuthApi.DB;
using AuthApi.Users.Entities;
using AuthApi.Users.Repositories.Interfaces;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Users.Repositories;

public class UserRepository(
    UserDbContext dbContext,
    ILoggerService logger) : BaseRepository<AppUser>(dbContext, logger), IUserRepository
{
    private readonly UserDbContext _dbContext = dbContext;
    private readonly ILoggerService _logger = logger;

    public async Task<AppUser?> GetUserByEmail(string email, CancellationToken token = default)
    {
        try
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(
                u => u.Email.ToLower() == email.ToLower(),
                token);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get user. Email Id: {email}.", ex);
            return null;
        }
    }
}
