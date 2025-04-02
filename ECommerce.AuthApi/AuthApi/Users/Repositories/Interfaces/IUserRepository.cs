using AuthApi.Users.Entities;
using ECommerce.Shared.Repositories.Interfaces;

namespace AuthApi.Users.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<AppUser>
{
    public Task<AppUser?> GetUserByEmail(string email, CancellationToken token = default);
}
