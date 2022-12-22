using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineWallet.Application.Contracts;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Infrastructure.Persistance;
using OnlineWallet.Infrastructure.Repositories;

namespace OnlineWallet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OnlineWalletDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("OnlineWalletConnectionString")));

        services.AddScoped<IOnlineWalletRepository, OnlineWalletRepository>();
        return services;
    }
}