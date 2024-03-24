using OnlineShop.Order.Api.Models.Responses;
using OnlineShop.Order.Api.Test.IntegrationTesting.ApplicationFactories;
using System.Net;
using System.Net.Http.Json;

namespace OnlineShop.Order.Api.Test.IntegrationTesting;

public class OrderTests(OrderWebApplicationFactory orderApplicationFactory, 
    CustomerWebApplicationFactory customerApplicationFactory,
    ProductWebApplicationFactory productApplicationFactory) 
    : IClassFixture<OrderWebApplicationFactory>, IClassFixture<CustomerWebApplicationFactory>, IClassFixture<ProductWebApplicationFactory>
{
    [Fact]
    public async Task Get_Order_Returns_All_Orders_Properly()
    {
        _ = customerApplicationFactory.CreateClient();
        _ = productApplicationFactory.CreateClient();
        var orderClient = orderApplicationFactory.CreateClient();

        var httpResponse = await orderClient.GetAsync("/order?from=2024-02-21T12:30:45.000Z&to=2024-04-25T12:30:45.000Z");

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        var response = await httpResponse.Content.ReadFromJsonAsync<List<OrderResponseModel>>();

        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Equal(3, response.Count);
        Assert.Equal(3, response[0].Products.Length);
        Assert.Equal(2, response[1].Products.Length);
        Assert.Single(response[2].Products);

        Assert.Multiple(() =>
        {
            Assert.DoesNotContain(null, response.Select(x => x.Customer?.Name));
            Assert.DoesNotContain(null, response.Select(x => x.Customer?.Surname));
            Assert.DoesNotContain(null, response.SelectMany(x => x.Products).Select(x => x.Definition));
        });
    }
}
