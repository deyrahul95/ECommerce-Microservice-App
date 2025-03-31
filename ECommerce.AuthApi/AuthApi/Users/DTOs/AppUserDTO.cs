namespace AuthApi.Users.DTOs;

public record AppUserDTO(
    Guid Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    string Role
);
