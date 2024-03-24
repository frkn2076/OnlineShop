using OnlineShop.Order.Api.Services.Implementations;
using OnlineShop.Order.Api.Test.UnitTesting.Database;

namespace OnlineShop.Order.Api.Test.UnitTesting;

[Collection("Database collection")]
public class OrderServiceTests(DatabaseFixture fixture) : OrderServiceBuilder
{
    [Fact]
    public async Task OrderService_Returns_OrderDetails_Properly()
    {
        var mockProductClient = GetMockProductClient();
        var mockCustomerClient = GetMockCustomerClient();

        var orderService = new OrderService(mockCustomerClient.Object, mockProductClient.Object, fixture.OrderDbContext);

        var orderDetails = await orderService.GetOrdersAsync(DateTime.Now.AddDays(-10), DateTime.Now);

        Assert.NotEmpty(orderDetails);
        Assert.Equal(3, orderDetails.Count());
        Assert.Equal(3, orderDetails.First().Products.Length);
        Assert.Equal(2, orderDetails.Skip(1).First().Products.Length);
        Assert.Single(orderDetails.Skip(2).First().Products);

        Assert.Multiple(() =>
        {
            Assert.DoesNotContain(null, orderDetails.Select(x => x.Customer?.Name));
            Assert.DoesNotContain(null, orderDetails.Select(x => x.Customer?.Surname));
            Assert.DoesNotContain(null, orderDetails.SelectMany(x => x.Products).Select(x => x.Definition));
        });
    }

    [Fact]
    public async Task OrderService_Returns_No_Orders_If_Not_Found()
    {
        var mockProductClient = GetMockProductClient();
        var mockCustomerClient = GetMockCustomerClient();

        var orderService = new OrderService(mockCustomerClient.Object, mockProductClient.Object, fixture.OrderDbContext);

        var orderDetails = await orderService.GetOrdersAsync(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));

        Assert.Empty(orderDetails);
    }
}
