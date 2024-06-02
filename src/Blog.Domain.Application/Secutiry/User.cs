using BuildingBlocks.Application.Security;

namespace Blog.Domain.Application.Secutiry
{
    public class User : IUser
    {
        public string Id => "a343e8ec-a3a9-460e-b486-3fb2e6b45404";

        public string Name => "Guest";

        public string[]? Roles => new string[1] { "Admin" };
    }
}