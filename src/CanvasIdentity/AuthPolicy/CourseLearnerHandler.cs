using System.Threading.Tasks;
using CanvasIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CanvasIdentity.AuthPolicy
{
    public class CourseLearnerHandler : AuthorizationHandler<CourseLearnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CourseLearnerRequirement requirement)
        {
            var canvasClaims = context.User.CanvasClaims();

            if (canvasClaims.IsCanvasLearner())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
