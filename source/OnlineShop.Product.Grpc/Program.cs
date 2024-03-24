using Microsoft.EntityFrameworkCore;
using OnlineShop.Product.Grpc.Data;
using OnlineShop.Product.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.Migrate();
}

app.MapGrpcService<ProductService>();
app.Run();