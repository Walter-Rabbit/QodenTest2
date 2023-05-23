using Microsoft.AspNetCore.Authorization;

namespace WebApp
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; }
    }
}