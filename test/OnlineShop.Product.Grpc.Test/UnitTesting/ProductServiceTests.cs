using Grpc.Core;
using Moq;
using OnlineShop.Product.Grpc.Services;
using OnlineShop.Product.Grpc.Test.UnitTesting.Database;

namespace OnlineShop.Product.Grpc.Test.UnitTesting;

[Collection("Database collection")]
public class ProductServiceTests(DatabaseFixture fixture)
{
    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public async Task ProductService_Returns_Products_If_Found(int orderId, int productCount)
    {
        var productService = new ProductService(fixture.ProductDbContext);

        var productRequest = new ProductRequest();
        productRequest.OrderIds.Add(orderId);

        var products = await productService.GetProducts(productRequest, Mock.Of<ServerCallContext>());

        Assert.NotNull(products);

        Assert.Multiple(() =>
        {
            Assert.Equal(productCount, products.Products.Count);
            Assert.DoesNotContain(null, products.Products.Select(x => x.Definition));
        });
    }
}
