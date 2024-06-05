using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Posts;

public class PostComment : Entity<PostComment>
{
    private PostComment(EntityId<PostComment> id, string content, bool isApproved, DateTime createdAt) : base(id)
    {
        Content = content;
        IsApproved = isApproved;
        CreatedAt = createdAt;
    }

    public string Content { get; }
    public bool IsApproved { get; private set; }
    public DateTime CreatedAt { get; }

    internal static PostComment Create(string content) => new PostComment(new EntityId<PostComment>(Guid.NewGuid()), content, false, DateTime.UtcNow);

    internal void Approve()
    {
        IsApproved = true;
    }
}