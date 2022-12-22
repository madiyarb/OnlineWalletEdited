using Identity.Application.Contracts;
using identity.Infrastructure.Persistance;
using identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace identity.Infrastructure;

public static class DenpendencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString")));

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}