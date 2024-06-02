using BuildingBlocks.Domain.Utility;

namespace BuildingBlocks.Domain.Entities;

public abstract class Entity<TEntity> where TEntity : Entity<TEntity>
{
    public EntityId<TEntity> Id { get; private init; }

    protected Entity()
    {
    }

    protected Entity(EntityId<TEntity> id)
    {
        Ensure.NotNull(id, "The identifier is required.", nameof(id));

        Id = id;
    }

    public static bool operator ==(Entity<TEntity> a, Entity<TEntity> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TEntity> a, Entity<TEntity> b) => !(a == b);

    /// <inheritdoc />
    public bool Equals(Entity<TEntity> other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (!(obj is Entity<TEntity> other))
        {
            return false;
        }

        if (Id.Value == Guid.Empty || other.Id.Value == Guid.Empty)
        {
            return false;
        }

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode() * 41;
}