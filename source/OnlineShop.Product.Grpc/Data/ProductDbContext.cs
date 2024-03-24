using Microsoft.EntityFrameworkCore;
using OnlineShop.Product.Grpc.Data.Entities;

namespace OnlineShop.Product.Grpc.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity
            {
                Id = 1,
                OrderId = 1,
                Definition = "Product1"
            },
            new ProductEntity
            {
                Id = 2,
                OrderId = 1,
                Definition = "Product2"
            },
            new ProductEntity
            {
                Id = 3,
                OrderId = 1,
                Definition = "Product3"
            },
            new ProductEntity
            {
                Id = 4,
                OrderId = 2,
                Definition = "Product4"
            },
            new ProductEntity
            {
                Id = 5,
                OrderId = 2,
                Definition = "Product5"
            },
            new ProductEntity
            {
                Id = 6,
                OrderId = 3,
                Definition = "Product6"
            }
        );
    }
}
