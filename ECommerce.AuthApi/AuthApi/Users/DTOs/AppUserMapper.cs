using AuthApi.Users.Entities;
using AuthApi.Users.Enums;
using AuthApi.Users.Models;

namespace AuthApi.Users.DTOs;

public static class AppUserMapper
{
    public static AppUserDTO ToDto(this AppUser user)
    {
        return new AppUserDTO(
            Id: user.Id,
            Name: user.Name,
            PhoneNumber: user.PhoneNumber,
            Email: user.Email,
            Address: user.Address ?? "N/A",
            Role: user.Role ?? "N/A"
        );
    }

    public static AppUser ToEntity(this RegisterRequest request)
    {
        return new AppUser { 
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Password = request.Password,
            Address = request.Address,
            Role = AppUserRole.User.ToString(),
            DateRegistered = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        };
    }
}
