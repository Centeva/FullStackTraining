using System.Reflection;
using FullStackTraining.Infrastructure;
using FullStackTraining.Seeder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException())
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructureServices(hostContext.Configuration);

        services.AddHostedService<DatabaseSeeder>();
    })
    .RunConsoleAsync();
