namespace AuthApi.Users.DTOs;

public record JWTTokenDTO(string Token, DateTime ExpiresIn);