using Blog.Domain.Domain.Categories;
using Blog.Domain.Domain.Users;
using BuildingBlocks.Domain.Entities;
using FluentResults;

namespace Blog.Domain.Domain.Posts;

public class Post : AggregateRoot<Post>
{
    private readonly IList<Category> _categories = new List<Category>();

    private readonly IList<PostTag> _tags = new List<PostTag>();

    private readonly IList<PostComment> _comments = new List<PostComment>();

    private readonly IList<PostReaction> _reactions = new List<PostReaction>();

    private Post(EntityId<Post> id, string title, string content, DateTime createdAt, PostStatus status, User author) : base(id)
    {
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        Status = status;
        Author = author;
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? PublishedAt { get; private set; }
    public User Author { get; }
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyCollection<PostTag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<PostComment> Comments => _comments.AsReadOnly();

    public IReadOnlyCollection<PostReaction> Reactions => _reactions.AsReadOnly();
    public PostStatus Status { get; private set; }

    public static Post Create(string title, string content, User author) => new Post(new EntityId<Post>(Guid.NewGuid()), title, content, DateTime.UtcNow, PostStatus.Draft, author);

    public void AddCategory(string name)
    {
        _categories.Add(Category.Create(name));
    }

    public void AddCategory(EntityId<Category> id, string name)
    {
        _categories.Add(Category.Create(id, name));
    }

    public Result RemoveCategory(EntityId<Category> id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);

        if (category is null) return Result.Fail(PostErrors.AttemptDeleteNonExistentCategory);

        _categories.Remove(category);

        return Result.Ok();
    }

    public void AddTag(string name)
    {
        _tags.Add(PostTag.Create(name));
    }

    public Result RemoveTag(EntityId<PostTag> id)
    {
        var category = _tags.FirstOrDefault(c => c.Id == id);

        if (category is null) return Result.Fail(PostErrors.AttemptDeleteNonExistentTag);

        _tags.Remove(category);

        return Result.Ok();
    }

    public void AddComment(string content)
    {
        _comments.Add(PostComment.Create(content));
    }

    public void ChangeTitle(string title)
    {
        Title = title;
    }

    public void ChangeContent(string title)
    {
        Title = title;
    }

    public Result ApproveComment(EntityId<PostComment> id)
    {
        PostComment comment = _comments.FirstOrDefault(c => c.Id.Equals(id))!;

        if (comment is null) return Result.Fail(PostErrors.AttemptApproveNonExistentComment);

        AddDomainEvent(new PostCommentApprovedEvent(comment.Id.Value, comment.Content));

        return Result.Ok();
    }

    public void Like()
    {
        _reactions.Add(PostReaction.Create(PostReactionType.Like));
        AddDomainEvent(new PostReactionEvent(PostReactionType.Like));
    }

    public void Dislike()
    {
        _reactions.Add(PostReaction.Create(PostReactionType.Dislike));
        AddDomainEvent(new PostReactionEvent(PostReactionType.Like));
    }

    public Result Publish()
    {
        if (PublishedAt is not null)
            return Result.Fail(PostErrors.AttemptPublishPostHasAlreadyPublished);

        Status = PostStatus.Published;
        PublishedAt = DateTime.UtcNow;

        AddDomainEvent(new PostPublishedEvent(Id.Value, Title));

        return Result.Ok();
    }
}