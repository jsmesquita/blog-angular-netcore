using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Posts;

public class Tag : Entity<Tag>
{
    private Tag(EntityId<Tag> id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static Tag Create(string name) => new Tag(new EntityId<Tag>(Guid.NewGuid()), name);
}