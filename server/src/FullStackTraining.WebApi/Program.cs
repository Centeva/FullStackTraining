using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.Infrastructure;
using FullStackTraining.Infrastructure.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructureServices(builder.Configuration);

// MediatR
builder.Services
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Project>())
    .AddSingleton<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

// Web stuff
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Create database if it doesn't exist (will not run migrations to bring existing DB up to date)
if (app.Environment.IsDevelopment())
{
    await using var serviceScope = app.Services.CreateAsyncScope();
    await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make the implicit Program.cs class public, so integration/functional tests can reference the correct assembly for host building
public partial class Program { }
