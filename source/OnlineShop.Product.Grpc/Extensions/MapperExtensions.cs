using OnlineShop.Product.Grpc.Data.Entities;

namespace OnlineShop.Product.Grpc.Extensions;

public static class MapperExtensions
{
    public static ProductResponse MapToProductResponse(this List<ProductEntity> products)
    {
        var productResponse = new ProductResponse();

        if (products is null || products.Count == 0)
        {
            return productResponse;
        }

        var productDetails = products.Select(x => new ProductDetail()
        {
            Id = x.Id,
            OrderId = x.OrderId,
            Definition = x.Definition
        });

        productResponse.Products.AddRange(productDetails);

        return productResponse;
    }
}
