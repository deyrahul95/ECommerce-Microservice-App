using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using OrderApi.Domain.Constants;

namespace OrderApi.Infrastructure.DB;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable(
                "Orders",
                t => t.HasCheckConstraint(
                    "CK_Order_PurchasedQuantity",
                    $"[PurchasedQuantity] >= {DBConstraint.PURCHASE_ORDER_MIN_QUANTITY} AND [PurchasedQuantity] <= {DBConstraint.PURCHASE_ORDER_MAX_QUANTITY}"));

            entity.ToTable(
                "Orders",
                t => t.HasCheckConstraint(
                    "CK_Order_OrderDate",
                    "OrderDate = CAST(GETDATE() AS DATE)"));
        });
    }
}
