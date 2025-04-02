using AuthApi.Users.DTOs;
using AuthApi.Users.Enums;
using AuthApi.Users.Repositories.Interfaces;
using AuthApi.Users.Services.Interfaces;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;

namespace AuthApi.Users.Services;

public class UserService(IUserRepository userRepository, ILoggerService logger) : IUserService
{
    public async Task<ServiceResult<List<AppUserDTO>>> GetAllUsers(CancellationToken token = default)
    {
        try
        {
            var users = await userRepository.GetAllAsync(token);

            var sortedUserDTOs = users.OrderByDescending(u => u.LastUpdated).ToDtoList();

            return UserResults<List<AppUserDTO>>.USER_FETCHED(sortedUserDTOs);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to fetch users data.", ex);
            return UserResults<List<AppUserDTO>>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<AppUserDTO>> GetUser(Guid id, CancellationToken token = default)
    {
        try
        {
            var user = await userRepository.FindByIdAsync(id, token);

            if (user is null || user.Id == Guid.Empty)
            {
                return UserResults<AppUserDTO>.USER_NOT_FOUND(id);
            }

            return UserResults<AppUserDTO>.USER_FETCHED(user.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to fetch user data. User ID: {id}", ex);
            return UserResults<AppUserDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<AppUserDTO>> MakeAdmin(Guid id, CancellationToken token = default)
    {
        try
        {
            var user = await userRepository.FindByIdAsync(id, token);

            if (user is null || user.Id == Guid.Empty)
            {
                return UserResults<AppUserDTO>.USER_NOT_FOUND(id);
            }

            if (user.Role == AppUserRole.Admin.ToString())
            {
                return UserResults<AppUserDTO>.USER_ALREADY_ADMIN(id);
            }

            user.Role = AppUserRole.Admin.ToString();
            user.LastUpdated = DateTime.UtcNow;

            var updatedUser = await userRepository.UpdateASync(user, token);

            if(updatedUser is null || updatedUser.Id == Guid.Empty)
            {
                return UserResults<AppUserDTO>.INTERNAL_SERVICE_FAILURE;
            }

            return UserResults<AppUserDTO>.USER_UPDATED(updatedUser.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to fetch user data. User ID: {id}", ex);
            return UserResults<AppUserDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }
}
