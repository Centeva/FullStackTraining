using Ardalis.GuardClauses;
using Centeva.DomainModeling;

namespace FullStackTraining.Core.ProjectAggregate;
public class Project : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public Project(Guid id, string name)
    {
        Id = Guard.Against.Default(id);
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    }

    public void Rename(string newName)
    {
        Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName));
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}
