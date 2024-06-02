namespace Blog.Domain.Domain.Posts
{
    public interface IPostRepository
    {
        Task CreateAsync(Post post);
    }
}