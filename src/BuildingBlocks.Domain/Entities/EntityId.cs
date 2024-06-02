namespace BuildingBlocks.Domain.Entities;

public record EntityId<TEntity>(Guid Value)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}