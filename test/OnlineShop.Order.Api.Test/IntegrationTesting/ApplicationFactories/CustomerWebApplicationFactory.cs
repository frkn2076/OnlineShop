using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using CustomerProgram = OnlineShop.Customer.Grpc.Program;

namespace OnlineShop.Order.Api.Test.IntegrationTesting.ApplicationFactories;

public class CustomerWebApplicationFactory : WebApplicationFactory<CustomerProgram>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // TODO: Url will be replaced the url of running instance in the test environment, run it locally before running the integration test.
        // Because integration tests start up an in-memory host not a real one. 
        builder.UseUrls("https://localhost:7089");
    }
}