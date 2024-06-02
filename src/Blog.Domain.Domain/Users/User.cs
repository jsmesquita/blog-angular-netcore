using BuildingBlocks.Domain.Entities;

namespace Blog.Domain.Domain.Users;

public class User : Entity<User>
{
    private User(EntityId<User> id, string username) : base(id)
    {
        Username = username;
    }

    public string Username { get; }

    public static User Create(EntityId<User> id, string username) => new User(id, username);
}