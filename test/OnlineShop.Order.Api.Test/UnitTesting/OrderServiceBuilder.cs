using Moq;
using OnlineShop.Customer.Grpc;
using OnlineShop.Order.Api.Test.UnitTesting.Comparers;
using OnlineShop.Order.Api.Test.UnitTesting.Helper;
using OnlineShop.Product.Grpc;

namespace OnlineShop.Order.Api.Test.UnitTesting;

public abstract class OrderServiceBuilder
{
    public Mock<Product.Grpc.Product.ProductClient> GetMockProductClient()
    {
        var mockProductClient = new Mock<Product.Grpc.Product.ProductClient>();
        
        var productRequest = new ProductRequest();
        productRequest.OrderIds.AddRange([1, 2, 3]);

        var productResponse = new ProductResponse();
        productResponse.Products.AddRange([
            new ProductDetail
            {
                Id = 1,
                OrderId = 1,
                Definition = "Product1"
            },
            new ProductDetail
            {
                Id = 2,
                OrderId = 1,
                Definition = "Product2"
            },
            new ProductDetail
            {
                Id = 3,
                OrderId = 1,
                Definition = "Product3"
            },
            new ProductDetail
            {
                Id = 4,
                OrderId = 2,
                Definition = "Product4"
            },
            new ProductDetail
            {
                Id = 5,
                OrderId = 2,
                Definition = "Product5"
            },
            new ProductDetail
            {
                Id = 6,
                OrderId = 3,
                Definition = "Product6"
            }]);

        mockProductClient
            .Setup(x => x.GetProductsAsync(It.Is(productRequest, BoxEqualityComparer.Create()), null, null, CancellationToken.None))
            .Returns(GrpcHelper.CreateAsyncUnaryCall(productResponse));

        return mockProductClient;
    }

    public Mock<Customer.Grpc.Customer.CustomerClient> GetMockCustomerClient()
    {
        var mockCustomerClient = new Mock<Customer.Grpc.Customer.CustomerClient>();

        var customerRequest = new CustomerRequest();
        customerRequest.Ids.AddRange([1, 2]);

        var customerResponse = new CustomerResponse();
        customerResponse.Customers.AddRange([
            new CustomerDetail
            {
                Id = 1,
                Name = "Jim",
                Surname = "Carrey"
            },
            new CustomerDetail
            {
                Id = 2,
                Name = "Ben",
                Surname = "Affleck"
            }]);

        mockCustomerClient
            .Setup(x => x.GetCustomersAsync(It.Is(customerRequest, CustomerRequestComparer.Create()), null, null, CancellationToken.None))
            .Returns(GrpcHelper.CreateAsyncUnaryCall(customerResponse));

        return mockCustomerClient;
    }
}
