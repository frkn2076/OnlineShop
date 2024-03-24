using Grpc.Core;
using Moq;
using OnlineShop.Customer.Grpc.Services;
using OnlineShop.Customer.Grpc.Test.UnitTesting.Database;

namespace OnlineShop.Customer.Grpc.Test.UnitTesting;

[Collection("Database collection")]
public class CustomerServiceTests(DatabaseFixture fixture)
{
    [Theory]
    [InlineData(1, "Jim", "Carrey")]
    [InlineData(2, "Ben", "Affleck")]
    public async Task CustomerService_Returns_Customers_Properly(int id, string name, string surname)
    {
        var orderService = new CustomerService(fixture.CustomerDbContext);

        var customerRequest = new CustomerRequest();
        customerRequest.Ids.Add(id);

        var customers = await orderService.GetCustomers(customerRequest, Mock.Of<ServerCallContext>());

        Assert.NotNull(customers);

        Assert.Multiple(() =>
        {
            Assert.Single(customers.Customers);
            Assert.Equal(id, customers.Customers[0].Id);
            Assert.Equal(name, customers.Customers[0].Name);
            Assert.Equal(surname, customers.Customers[0].Surname);
        });
    }

    [Fact]
    public async Task CustomerService_Returns_No_Customers_If_Not_Found()
    {
        var orderService = new CustomerService(fixture.CustomerDbContext);

        var customerRequest = new CustomerRequest();
        customerRequest.Ids.Add(3);

        var customers = await orderService.GetCustomers(customerRequest, Mock.Of<ServerCallContext>());

        Assert.NotNull(customers);
        Assert.Empty(customers.Customers);
    }
}