using Ardalis.GuardClauses;
using Centeva.DomainModeling;
using Centeva.DomainModeling.EFCore;
using FullStackTraining.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackTraining.Infrastructure;
public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Guard.Against.NullOrWhiteSpace(configuration.GetConnectionString("Default"));

        services.AddSingleton<DispatchDomainEventsInterceptor>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options.UseSqlServer(connectionString)
                .AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>())
        );

        return services;
    }
}
