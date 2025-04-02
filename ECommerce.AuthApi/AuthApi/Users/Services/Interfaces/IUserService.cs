using AuthApi.Users.DTOs;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services.Interfaces;

public interface IUserService
{
    public Task<ServiceResult<AppUserDTO>> GetUser(Guid id, CancellationToken token = default);
    public Task<ServiceResult<List<AppUserDTO>>> GetAllUsers(CancellationToken token = default);
    public Task<ServiceResult<AppUserDTO>> MarkedAsAdmin(Guid id, CancellationToken token = default);
}
