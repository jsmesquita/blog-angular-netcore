namespace Blog.Domain.Domain.Categories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
    }
}