using BuildingBlocks.Domain.Entities;

namespace BuildingBlocks.Domain.Repositories;

public interface IWriteRepository<TEntity> where TEntity : Entity<TEntity>, new()
{
    Task<TEntity> CreateAsync(TEntity model);

    Task<TEntity> UpdateAsync(string id, TEntity model);

    Task DeleteAsync(string id);
}