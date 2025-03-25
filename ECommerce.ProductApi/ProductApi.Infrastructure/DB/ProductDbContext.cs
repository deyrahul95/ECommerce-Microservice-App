using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Constants;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.DB;

public class ProductDbContext(DbContextOptions options):DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable(
                "Products",
                t => t.HasCheckConstraint(
                    "CK_Product_Quantity",
                    $"[Quantity] >= {DBConstraint.PRODUCT_MIN_QUANTITY} AND [Quantity] <= {DBConstraint.PRODUCT_MAX_QUANTITY}"));

            entity.ToTable(
                "Products",
                t => t.HasCheckConstraint(
                    "CK_Product_Price",
                    $"[Price] >= {DBConstraint.PRODUCT_MIN_PRICE} AND [Price] <= {DBConstraint.PRODUCT_MAX_PRICE}"));

            entity.HasIndex(p => p.Name).IsUnique();
        });

        // Seed some default data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product A",
                Quantity = 10,
                Price = 99.99m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product B",
                Quantity = 20,
                Price = 199.99m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product C",
                Quantity = 30,
                Price = 299.99m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product D",
                Quantity = 40,
                Price = 399.99m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
