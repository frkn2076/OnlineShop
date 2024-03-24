using OnlineShop.Customer.Grpc;

namespace OnlineShop.Order.Api.Test.UnitTesting.Comparers;

class CustomerRequestComparer : IEqualityComparer<CustomerRequest>
{
    public static CustomerRequestComparer Create() => new();

    public bool Equals(CustomerRequest x, CustomerRequest y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Ids.OrderBy(a => a).SequenceEqual(y.Ids.OrderBy(a => a));
    }

    public int GetHashCode(CustomerRequest productRequest)
    {
        if (productRequest is null)
        {
            return 0;
        }

        return productRequest.Ids.GetHashCode();
    }
}
