using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Posts;

public class PostReaction : Entity<PostReaction>
{
    private PostReaction(EntityId<PostReaction> id, PostReactionType reactionType, DateTime reactedAt) : base(id)
    {
        ReactionType = reactionType;
        ReactedAt = reactedAt;
    }

    public PostReactionType ReactionType { get; }

    public DateTime ReactedAt { get; }

    public static PostReaction Create(PostReactionType reactionType) => new PostReaction(new EntityId<PostReaction>(Guid.NewGuid()), reactionType, DateTime.UtcNow);
}