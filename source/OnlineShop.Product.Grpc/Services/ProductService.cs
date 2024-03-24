using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Product.Grpc.Data;
using OnlineShop.Product.Grpc.Extensions;

namespace OnlineShop.Product.Grpc.Services;

public class ProductService(ProductDbContext productDbContext) : Product.ProductBase
{
    public override async Task<ProductResponse> GetProducts(ProductRequest request, ServerCallContext context)
    {
        var products = await productDbContext.Products
            .Where(x => request.OrderIds.Contains(x.OrderId))
            .ToListAsync(context.CancellationToken);

        var response = products.MapToProductResponse();
        return response;
    }
}
