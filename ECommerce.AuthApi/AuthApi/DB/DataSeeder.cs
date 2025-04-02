using AuthApi.Users.Entities;

namespace AuthApi.DB;

public static class DataSeeder
{
    public static void SeedData(UserDbContext dbContext)
    {
        if (dbContext.Users.Any())
        {
            return;
        }

        var users = new List<AppUser>
        {
            new()
            {
                Name = "Admin User",
                Email = "admin@example.com",
                PhoneNumber = "1111111111",
                Address = "Admin address not found!",
                Password = BCrypt.Net.BCrypt.HashPassword("AdminPassword"),
                Role = "Admin",
            },
            new()
            {
                Name = "Regular User",
                Email = "user@example.com",
                PhoneNumber = "2222222222",
                 Address = "User address not found!",
                Password = BCrypt.Net.BCrypt.HashPassword("UserPassword"),
                Role = "User",
            },
        };

        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
    }
}
