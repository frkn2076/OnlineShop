using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Api.Data;
using OnlineShop.Order.Api.Data.Entities;

namespace OnlineShop.Order.Api.Test.UnitTesting.Database;

public class DatabaseFixture : IDisposable
{
    public OrderDbContext OrderDbContext { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        OrderDbContext = new OrderDbContext(options);

        OrderDbContext.Orders.AddRange(new List<OrderEntity>()
        {
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
        });

        OrderDbContext.SaveChanges();
    }

    public void Dispose()
    {
        OrderDbContext.Dispose();
    }
}
