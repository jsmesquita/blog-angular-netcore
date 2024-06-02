using Blog.Domain.Domain.Posts;

namespace Blog.Domain.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task CreateAsync(Post post)
        {
            return Task.CompletedTask;
        }
    }
}