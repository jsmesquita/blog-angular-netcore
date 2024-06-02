using BuildingBlocks.Domain.Entities;

namespace BuildingBlocks.Domain.Repositories;

public interface IReadRepository<TEntity> where TEntity : Entity<TEntity>, new()
{
    Task<IEnumerable<TEntity>> AllAsync();

    Task<TEntity> FindAsync(string id);
}