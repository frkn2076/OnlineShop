using Microsoft.EntityFrameworkCore;
using OnlineShop.Customer.Grpc;
using OnlineShop.Order.Api.Data;
using OnlineShop.Order.Api.Models.Responses;
using OnlineShop.Order.Api.Services.Contracts;
using OnlineShop.Product.Grpc;

namespace OnlineShop.Order.Api.Services.Implementations;

public class OrderService(Customer.Grpc.Customer.CustomerClient customerClient,
    Product.Grpc.Product.ProductClient productClient,
    OrderDbContext orderDbContext) : IOrderService
{
    public async Task<IEnumerable<OrderResponseModel>> GetOrdersAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        var orders = await orderDbContext.Orders
            .Where(x => x.OrderedAt >= from && x.OrderedAt <= to)
            .ToListAsync(cancellationToken);

        if (orders is null || orders.Count == 0)
        {
            return Enumerable.Empty<OrderResponseModel>();
        }

        var customerRequest = new CustomerRequest();
        customerRequest.Ids.AddRange(orders.Select(x => x.CustomerId).Distinct());

        var productRequest = new ProductRequest();
        productRequest.OrderIds.AddRange(orders.Select(x => x.Id).Distinct());

        var customerResponseTask = customerClient.GetCustomersAsync(customerRequest, cancellationToken: cancellationToken);
        var productResponseTask = productClient.GetProductsAsync(productRequest, cancellationToken: cancellationToken);

        await Task.WhenAll(customerResponseTask.ResponseAsync, productResponseTask.ResponseAsync);

        var customerResponse = await customerResponseTask;
        var productResponse = await productResponseTask;

        var orderResponses = new List<OrderResponseModel>(orders.Count);

        foreach (var order in orders)
        {
            var orderResponse = new OrderResponseModel()
            {
                Id = order.Id,
                Amount = order.Amount,
                OrderedAt = order.OrderedAt,
            };

            var customerDetails = customerResponse.Customers.Single(c => c.Id == order.CustomerId);

            orderResponse.Customer = new CustomerResponseModel()
            {
                Id = customerDetails.Id,
                Name = customerDetails.Name,
                Surname = customerDetails.Surname
            };

            var productDetails = productResponse.Products.Where(x => x.OrderId == order.Id).ToArray();

            orderResponse.Products = productDetails.Select(x => new ProductResponseModel()
            {
                Id = x.Id,
                Definition = x.Definition
            }).ToArray();

            orderResponses.Add(orderResponse);
        }

        return orderResponses;
    }
}
