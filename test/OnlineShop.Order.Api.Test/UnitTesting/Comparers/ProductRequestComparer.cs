using OnlineShop.Product.Grpc;

namespace OnlineShop.Order.Api.Test.UnitTesting.Comparers;

class BoxEqualityComparer : IEqualityComparer<ProductRequest>
{
    public static BoxEqualityComparer Create() => new();

    public bool Equals(ProductRequest x, ProductRequest y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.OrderIds.OrderBy(a => a).SequenceEqual(y.OrderIds.OrderBy(a => a));
    }

    public int GetHashCode(ProductRequest productRequest)
    {
        if (productRequest is null)
        {
            return 0;
        }

        return productRequest.OrderIds.GetHashCode();
    }
}
