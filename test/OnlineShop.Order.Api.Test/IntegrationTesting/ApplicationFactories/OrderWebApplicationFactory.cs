using Microsoft.AspNetCore.Mvc.Testing;
using OrderProgram = OnlineShop.Order.Api.Program;

namespace OnlineShop.Order.Api.Test.IntegrationTesting.ApplicationFactories;

public class OrderWebApplicationFactory : WebApplicationFactory<OrderProgram>
{
}