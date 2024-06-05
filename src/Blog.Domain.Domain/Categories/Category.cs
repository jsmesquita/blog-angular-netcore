using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Categories;

public class Category : Entity<Category>
{
    private Category(EntityId<Category> id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static Category Create(string name) => new Category(new EntityId<Category>(Guid.NewGuid()), name);

    public static Category Create(EntityId<Category> id, string name) => new Category(id, name);
}