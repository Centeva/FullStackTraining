using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FullStackTraining.WebApi.Projects;

public class ProjectsController : ApiControllerBase
{
    private readonly IRepository<Project> _projectRepository;

    public ProjectsController(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectRepository.ListAsync();

        var response = projects.Select(x => new ProjectDto(x.Id, x.Name, x.Description));

        return Ok(response);
    }
}
