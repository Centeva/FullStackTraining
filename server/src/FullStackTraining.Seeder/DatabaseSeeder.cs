using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FullStackTraining.Seeder;
internal sealed class DatabaseSeeder : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger<DatabaseSeeder> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IRepository<Project> _projectRepository;

    public DatabaseSeeder(IHostApplicationLifetime lifetime, ILogger<DatabaseSeeder> logger, ApplicationDbContext dbContext, IRepository<Project> projectRepository)
    {
        _lifetime = lifetime;
        _logger = logger;
        _dbContext = dbContext;
        _projectRepository = projectRepository;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

        await CreateSampleProject();

        _lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task CreateSampleProject()
    {
        if (!await _projectRepository.AnyAsync())
        {
            _logger.LogInformation("Creating sample project");

            var sampleProject = new Project(Guid.Parse("f9bb9f41-1706-4930-893a-94cc1d226dc0"), "Sample Project");
            sampleProject.UpdateDescription("This is a sample project.  You can safely delete this.");

            await _projectRepository.AddAsync(sampleProject);
        }
    }
}
