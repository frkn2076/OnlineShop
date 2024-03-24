using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Api.Data.Entities;

namespace OnlineShop.Order.Api.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>().HasData(
            new OrderEntity
            {
                Id = 1,
                CustomerId = 1,
                Amount = 50.4M,
                OrderedAt = DateTime.UtcNow.AddDays(-1)
            },
            new OrderEntity
            {
                Id = 2,
                CustomerId = 1,
                Amount = 25.1M,
                OrderedAt = DateTime.UtcNow.AddDays(-3)
            },
            new OrderEntity
            {
                Id = 3,
                CustomerId = 2,
                Amount = 10.2M,
                OrderedAt = DateTime.UtcNow.AddDays(-1)
            }
        );
    }
}
