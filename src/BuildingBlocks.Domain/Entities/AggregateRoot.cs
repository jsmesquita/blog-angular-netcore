using BuildingBlocks.Domain.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingBlocks.Domain.Entities;

public abstract class AggregateRoot<TEntity> : Entity<TEntity> where TEntity : AggregateRoot<TEntity>
{
    private readonly List<BaseEvent> _domainEvents = new();

    protected AggregateRoot(EntityId<TEntity> id) : base(id)
    {
    }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}