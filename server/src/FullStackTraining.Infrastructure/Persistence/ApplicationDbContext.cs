using FullStackTraining.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;

namespace FullStackTraining.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
