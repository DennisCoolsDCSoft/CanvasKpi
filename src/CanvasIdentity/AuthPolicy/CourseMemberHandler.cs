using System.Linq;
using System.Threading.Tasks;
using CanvasIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace CanvasIdentity.AuthPolicy
{
    public class CourseMemberHandler : AuthorizationHandler<CourseMemberRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CourseMemberRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == LtiClaimsViewModel.ClaimName.CourseRequest))
            {
                return Task.CompletedTask;
            }
            var courseNrClaim = context.User.FindFirst(c => c.Type == LtiClaimsViewModel.ClaimName.CourseRequest);
            if (context.User.Claims.Any(c => c.Issuer == courseNrClaim.Value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
