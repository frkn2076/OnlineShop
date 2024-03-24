using Microsoft.EntityFrameworkCore;
using OnlineShop.Customer.Grpc.Data.Entities;

namespace OnlineShop.Customer.Grpc.Data;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>().HasData(
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
        );
    }
}
