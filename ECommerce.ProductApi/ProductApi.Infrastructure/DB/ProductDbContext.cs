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
    }
}
