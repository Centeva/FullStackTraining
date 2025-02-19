using FullStackTraining.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FullStackTraining.FunctionalTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly SqliteConnection _connection;

        public CustomWebApplicationFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("development");
            var host = builder.Build();
            host.Start();

            var serviceProvider = host.Services;

            using var scope = serviceProvider.CreateScope();

            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

            // Reset Sqlite database for each test run
            // If using a real database, you'll likely want to remove this step.
            db.Database.EnsureDeleted();

            // Ensure the database is created.
            db.Database.EnsureCreated();

            try
            {
                // Can also skip creating the items
                //if (!db.ToDoItems.Any())
                //{
                // Seed the database with test data.
                SeedData.PopulateTestDataAsync(db).Wait();
                //}
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test data. " +
                                    "Error: {exceptionMessage}", ex.Message);
            }

            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                UseInMemoryDatabase(services);

                // Register any other test-specific services here
            });
        }

        private void UseInMemoryDatabase(IServiceCollection services)
        {
            var descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
        }
    }
}
