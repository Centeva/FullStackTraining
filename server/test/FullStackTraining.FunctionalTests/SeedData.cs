using FullStackTraining.Core.ProjectAggregate;
using FullStackTraining.Infrastructure.Persistence;

namespace FullStackTraining.FunctionalTests
{
    internal static class SeedData
    {
        public static readonly Project Project1 = new(Guid.NewGuid(), "Test Project 1");
        public static readonly Project Project2 = new(Guid.NewGuid(), "Test Project 2");

        public static async Task PopulateTestDataAsync(ApplicationDbContext dbContext)
        {
            foreach (var project in dbContext.Projects)
            {
                dbContext.Remove(project);
            }

            await dbContext.SaveChangesAsync();

            dbContext.Projects.AddRange(Project1, Project2);

            await dbContext.SaveChangesAsync();
        }
    }
}
