using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructureServices(builder.Configuration);

// MediatR
builder.Services
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Project>())
    .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
