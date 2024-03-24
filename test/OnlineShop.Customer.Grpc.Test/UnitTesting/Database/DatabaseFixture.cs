using Microsoft.EntityFrameworkCore;
using OnlineShop.Customer.Grpc.Data;
using OnlineShop.Customer.Grpc.Data.Entities;

namespace OnlineShop.Customer.Grpc.Test.UnitTesting.Database;

public class DatabaseFixture : IDisposable
{
    public CustomerDbContext CustomerDbContext { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<CustomerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        CustomerDbContext = new CustomerDbContext(options);

        CustomerDbContext.Customers.AddRange(new List<CustomerEntity>()
        {
            new CustomerEntity
            {
                Id = 1,
                Name = "Jim",
                Surname = "Carrey"
            },
            new CustomerEntity
            {
                Id = 2,
                Name = "Ben",
                Surname = "Affleck"
            }
        });

        CustomerDbContext.SaveChanges();
    }

    public void Dispose()
    {
        CustomerDbContext.Dispose();
    }
}
