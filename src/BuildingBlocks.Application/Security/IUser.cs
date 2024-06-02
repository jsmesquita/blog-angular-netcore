namespace BuildingBlocks.Application.Security;

public interface IUser
{
    string Id { get; }

    string Name { get; }

    string[]? Roles { get; }
}