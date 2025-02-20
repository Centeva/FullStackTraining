using Ardalis.HttpClientTestExtensions;
using FullStackTraining.WebApi.Projects;

namespace FullStackTraining.FunctionalTests.ApiEndpoints
{
    public class ProjectList(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task ReturnsProjects()
        {
            var result = await _client.GetAndDeserializeAsync<List<ProjectDto>>("/api/projects");

            result.Count.ShouldBe(2);
            result.ShouldContain(p => p.Id == SeedData.Project1.Id);
            result.ShouldContain(p => p.Id == SeedData.Project2.Id);
        }
    }
}
