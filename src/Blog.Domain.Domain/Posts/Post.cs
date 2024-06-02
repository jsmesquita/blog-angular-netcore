using Blog.Domain.Domain.Users;
using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Posts;

public class Post : AggregateRoot<Post>
{
    private readonly IList<Category> _categories = new List<Category>();

    private readonly IList<Tag> _tags = new List<Tag>();

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
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public PostStatus Status { get; private set; }

    public static Post Create(string title, string content, User author) => new Post(new EntityId<Post>(Guid.NewGuid()), title, content, DateTime.UtcNow, PostStatus.Draft, author);

    public void AddCategory(string name)
    {
        _categories.Add(Category.Create(name));
    }

    public void RemoveCategory(EntityId<Category> id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);

        if (category is null) return;

        _categories.Remove(category);
    }

    public void AddTag(string name)
    {
        _tags.Add(Tag.Create(name));
    }

    public void RemoveTag(EntityId<Tag> id)
    {
        var category = _tags.FirstOrDefault(c => c.Id == id);

        if (category is null) return;

        _tags.Remove(category);
    }

    public void ChangeTitle(string title)
    {
        Title = title;
    }

    public void ChangeContent(string title)
    {
        Title = title;
    }

    public void Publish()
    {
        Status = PostStatus.Published;
        PublishedAt = DateTime.UtcNow;
    }
}