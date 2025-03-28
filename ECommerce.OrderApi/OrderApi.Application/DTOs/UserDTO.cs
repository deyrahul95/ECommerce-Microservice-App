namespace OrderApi.Application.DTOs;

public record UserDTO(
    Guid Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    string Role
);
