using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Customer.Grpc.Data;
using OnlineShop.Customer.Grpc.Services;

namespace OnlineShop.Customer.Grpc;

public partial class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();
        builder.Services.AddDbContext<CustomerDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresContext")));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
            dbContext.Database.Migrate();
        }

        app.MapGrpcService<CustomerService>();
        app.Run();
    }
}