using AuthApi.Users.Entities;

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
}
