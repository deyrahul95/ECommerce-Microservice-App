using System.ComponentModel.DataAnnotations;
using AuthApi.Users.Configs;

namespace AuthApi.Users.Entities;

public class AppUser
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(DBConstraints.NAME_MAX_LENGTH, MinimumLength = DBConstraints.NAME_MIN_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(DBConstraints.PHONE_NUMBER_LENGTH, MinimumLength = DBConstraints.PHONE_NUMBER_LENGTH)]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(DBConstraints.EMAIL_MAX_LENGTH)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(DBConstraints.PASSWORD_MAX_LENGTH, MinimumLength = DBConstraints.PASSWORD_MIN_LENGTH)]
    public string Password { get; set; } = string.Empty;

    [StringLength(DBConstraints.ADDRESS_MAX_LENGTH)]
    public string? Address { get; set; }
    public string? Role { get; set; }

    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
