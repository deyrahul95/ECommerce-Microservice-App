using AuthApi.Users.Models;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services.Interfaces;

public interface IAuthService
{
    public Task<ServiceResult> Register(RegisterRequest request, CancellationToken token = default);
    public Task<ServiceResult<string>> Login(LoginRequest request, CancellationToken token = default);
}
