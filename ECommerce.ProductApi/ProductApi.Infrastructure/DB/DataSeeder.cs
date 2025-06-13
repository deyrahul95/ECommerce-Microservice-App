using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.DB;

public static class DataSeeder
{
    public static void SeedData(ProductDbContext dbContext)
    {
        if (dbContext.Products.Any())
        {
            return;
        }

        var products = new List<Product>()
        {
            new(){
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S25 5G",
                Quantity = 26,
                Price = 56049.26m
            },
            new(){
                Id = Guid.NewGuid(),
                Name = "Samsung Tab S10 Ultra",
                Quantity = 15,
                Price = 96089.26m
            },
            new(){
                Id = Guid.NewGuid(),
                Name = "Samsung A35 5G",
                Quantity = 35,
                Price = 26049.26m
            },
            new(){
                Id = Guid.NewGuid(),
                Name = "Dell Ultra Slim Laptop",
                Quantity = 12,
                Price = 96094.26m
            },
        };

        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
    }
}
