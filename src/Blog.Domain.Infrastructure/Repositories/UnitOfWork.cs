using BuildingBlocks.Domain.Repositories;

namespace Blog.Domain.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}