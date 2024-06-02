using BuildingBlocks.Domain.Enums;

namespace Blog.Domain.Domain.Posts;
public record PostStatus : Enumeration<PostStatus>
{
    public static readonly PostStatus Draft = new(1, nameof(Draft));
    public static readonly PostStatus Published = new(2, nameof(Published));
    public static readonly PostStatus Scheduled = new(3, nameof(Scheduled));

    private PostStatus(int key, string value) : base(key, value)
    {
    }
}