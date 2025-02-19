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
        services.AddSingleton<DispatchDomainEventsInterceptor>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "FullStackTraining.db");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options.UseSqlite($"Data Source={dbPath}")
                .AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>())
        );

        return services;
    }
}
