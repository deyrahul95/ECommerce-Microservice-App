namespace AuthApi.Users.Models;

public record JwtClaimsData(
    string? UserId,
    string? Name,
    string? Email,
    List<string> Roles
);
