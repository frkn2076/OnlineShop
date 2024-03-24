using Microsoft.EntityFrameworkCore;
using OnlineShop.Product.Grpc.Data;
using OnlineShop.Product.Grpc.Data.Entities;

namespace OnlineShop.Product.Grpc.Test.UnitTesting.Database;

public class DatabaseFixture : IDisposable
{
    public ProductDbContext ProductDbContext { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        ProductDbContext = new ProductDbContext(options);

        ProductDbContext.Products.AddRange(new List<ProductEntity>()
        {
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
        });

        ProductDbContext.SaveChanges();
    }

    public void Dispose()
    {
        ProductDbContext.Dispose();
    }
}
