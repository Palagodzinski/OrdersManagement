using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersManagement.Application;
using OrdersManagement.Application.Interfaces;
using OrdersManagement.Application.Services;
using OrdersManagement.Infrastructure.Data;
using OrdersManagement.Infrastructure.Interfaces;
using OrdersManagement.Infrastructure.Repositories;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var billingService = host.Services.GetRequiredService<IBillingService>();

        await billingService.AddBillingEntriesByOrderIdAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ApiConfiguration.Initialize(context.Configuration);

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(ApiConfiguration.connectionString));
                    services.AddAutoMapper(typeof(Mappings));
                    services.AddHttpClient();
                    services.AddTransient<IBillingService, BillingService>();
                    services.AddTransient<IBillingRepository, BillingRepository>();
                    services.AddTransient<IOrderRepository, OrderRepository>();
                    services.AddTransient<IAuthorizationService, AuthorizationService>();
                    services.AddTransient<IOfferRepository, OfferRepository>();
                });
}