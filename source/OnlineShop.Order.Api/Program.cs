using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Api.Data;
using OnlineShop.Order.Api.Extensions;
using OnlineShop.Order.Api.Services.Contracts;
using OnlineShop.Order.Api.Services.Implementations;
using OnlineShop.Order.Api.Utils;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.Order.Api;

public partial class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddOptions<GrpcChannels>()
            .Bind(builder.Configuration.GetSection(nameof(GrpcChannels)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var grpcChannels = builder.Configuration.GetOptions<GrpcChannels>();

        builder.Services.AddGrpcClient<Customer.Grpc.Customer.CustomerClient>(o =>
        {
            o.Address = new Uri(grpcChannels.Customer.Url);
        })
        .ConfigurePrimaryHttpMessageHandler(() => CreateCertificatedHttpClientHandler("https", "OnlineShop.Customer.Grpc.pfx", grpcChannels.Customer.CertificatePassword));

        builder.Services.AddGrpcClient<Product.Grpc.Product.ProductClient>(o =>
        {
            o.Address = new Uri(grpcChannels.Product.Url);
        })
        .ConfigurePrimaryHttpMessageHandler(() => CreateCertificatedHttpClientHandler("https", "OnlineShop.Product.Grpc.pfx", grpcChannels.Product.CertificatePassword));


        builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresContext")));

        builder.Services.AddScoped<IOrderService, OrderService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                Console.WriteLine(exception);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync("Something went wrong!");
                await Task.CompletedTask;
            });
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static HttpClientHandler CreateCertificatedHttpClientHandler(string path, string certificateName, string password)
    {
        var userProfileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var grpcCertificatePath = Path.Combine(userProfileDirectory, path, certificateName);
        var customerGrpcCertificate = new X509Certificate2(grpcCertificatePath, password);
        
        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(customerGrpcCertificate);
        return handler;
    }
}
