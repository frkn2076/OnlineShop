using OnlineShop.Customer.Grpc.Data.Entities;

namespace OnlineShop.Customer.Grpc.Extensions;

public static class MapperExtensions
{
    public static CustomerResponse MapToCustomerResponse(this List<CustomerEntity> customers)
    {
        var customerResponse = new CustomerResponse();

        if (customers is null || customers.Count == 0)
        {
            return customerResponse;
        }

        var customerDetails = customers.Select(x => new CustomerDetail()
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname
        });

        customerResponse.Customers.AddRange(customerDetails);

        return customerResponse;
    }
}
