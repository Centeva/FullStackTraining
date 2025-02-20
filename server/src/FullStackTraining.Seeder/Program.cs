using System.Reflection;
using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.Infrastructure;
using FullStackTraining.Seeder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException())
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Project>())
            .AddSingleton<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        services.AddInfrastructureServices(hostContext.Configuration);

        services.AddHostedService<DatabaseSeeder>();
    })
    .RunConsoleAsync();
