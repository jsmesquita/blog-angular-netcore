using BuildingBlocks.Application.Security;

namespace Blog.Domain.Application.Secutiry
{
    public class IdentityService : IIdentityService
    {
        public async ValueTask<bool> AuthorizeAsync(string userId, string policyName)
        {
            return true;
        }

        public async ValueTask<string?> GetUserNameAsync(string userId)
        {
            return "Guest";
        }

        public async ValueTask<bool> IsInRoleAsync(string userId, string role)
        {
            return true;
        }
    }
}