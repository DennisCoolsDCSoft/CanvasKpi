using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CanvasIdentity.Extensions;

namespace CanvasIdentity.AuthPolicy
{
    public class CourseInstructorHandler : AuthorizationHandler<CourseInstructorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CourseInstructorRequirement requirement)
        {
            var canvasClaims = context.User.CanvasClaims();
            
            if (canvasClaims.IsCanvasInstructor())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
