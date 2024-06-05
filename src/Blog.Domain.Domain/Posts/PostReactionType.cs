using BuildingBlocks.Domain.Enums;

namespace Blog.Domain.Domain.Posts;

public record PostReactionType : Enumeration<PostReactionType>
{
    public static readonly PostReactionType Like = new(1, nameof(Like));
    public static readonly PostReactionType Dislike = new(2, nameof(Dislike));

    private PostReactionType(int key, string value) : base(key, value)
    {
    }
}