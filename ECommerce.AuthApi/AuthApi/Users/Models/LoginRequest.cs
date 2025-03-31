using System.ComponentModel.DataAnnotations;

namespace AuthApi.Users.Models;

public record LoginRequest(
    [Required][EmailAddress] string Email,
    [Required] string Password
);
