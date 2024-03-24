using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Customer.Grpc.Data;
using OnlineShop.Customer.Grpc.Extensions;

namespace OnlineShop.Customer.Grpc.Services;

public class CustomerService(CustomerDbContext customerDbContext) : Customer.CustomerBase
{
    public override async Task<CustomerResponse> GetCustomers(CustomerRequest request, ServerCallContext context)
    {
        var customers = await customerDbContext.Customers
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync(context.CancellationToken);
        
        var response = customers.MapToCustomerResponse();
        return response;
    }
}
