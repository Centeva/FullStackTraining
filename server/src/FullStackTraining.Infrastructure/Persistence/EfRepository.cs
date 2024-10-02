using Centeva.DomainModeling;
using Centeva.DomainModeling.EFCore;

namespace FullStackTraining.Infrastructure.Persistence;
public class EfRepository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}