namespace BuildingBlocks.Application.Security;

public interface IIdentityService
{
    ValueTask<string?> GetUserNameAsync(string userId);

    ValueTask<bool> IsInRoleAsync(string userId, string role);

    ValueTask<bool> AuthorizeAsync(string userId, string policyName);
}