using System.ComponentModel.DataAnnotations;
using AuthApi.Users.Entities;

namespace AuthApi.Users.Models;

public record RegisterRequest(
    [Required][MinLength(DBConstraints.NAME_MIN_LENGTH)][MaxLength(DBConstraints.NAME_MAX_LENGTH)] string Name,
    [Required][MinLength(DBConstraints.PHONE_NUMBER_LENGTH)][MaxLength(DBConstraints.PHONE_NUMBER_LENGTH)][Phone] string PhoneNumber,
    [Required][MaxLength(DBConstraints.EMAIL_MAX_LENGTH)][EmailAddress] string Email,
    [Required][MinLength(DBConstraints.PASSWORD_MIN_LENGTH)][MaxLength(DBConstraints.PASSWORD_MAX_LENGTH)] string Password,
    [MaxLength(DBConstraints.ADDRESS_MAX_LENGTH)] string? Address = null
);
