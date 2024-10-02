using Centeva.DomainModeling;
using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.WebApi.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace FullStackTraining.WebApi.Controllers;

public class ProjectsController : ApiControllerBase
{
    private readonly IRepository<Project> _projectRepository;

    public ProjectsController(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet("", Name = nameof(GetProjects))]
    public async Task<IActionResult> GetProjects()
    {
        var projects = (await _projectRepository.ListAsync())
            .Select(x => new ProjectModel(x.Id, x.Name, x.Description));

        return Ok(projects);
    }
}
