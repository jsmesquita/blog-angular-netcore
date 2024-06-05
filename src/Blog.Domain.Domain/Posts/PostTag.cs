using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Posts;

public class PostTag : Entity<PostTag>
{
    private PostTag(EntityId<PostTag> id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static PostTag Create(string name) => new PostTag(new EntityId<PostTag>(Guid.NewGuid()), name);
}