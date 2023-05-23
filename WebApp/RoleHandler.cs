using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApp
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IAccountService _accountService;

        public RoleHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            var externalId = context.User.Claims.FirstOrDefault(c => c.Type == "ExternalId")?.Value;

            if (externalId is null)
            {
                context.Fail();
                return;
            }
            
            var account = await _accountService.LoadOrCreateAsync(externalId);

            if (account.Role != null && account.Role == requirement.Role)
            {
                context.Succeed(requirement);
                return;
            }
            
            context.Fail();
        }
    }
}