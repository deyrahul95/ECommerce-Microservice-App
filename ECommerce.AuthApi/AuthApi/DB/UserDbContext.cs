using AuthApi.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.DB;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>( entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.PhoneNumber).IsUnique();
        });
    }
}
