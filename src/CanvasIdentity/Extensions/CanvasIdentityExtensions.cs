using System;
using CanvasIdentity.ExceptionPage;
using Microsoft.AspNetCore.Builder;
using CanvasIdentity.Middleware;
using CanvasIdentity.Models;

namespace CanvasIdentity.Extensions
{
    public static class CanvasIdentityExtensions
    {
        public static IApplicationBuilder UseCanvasUseExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(ExceptionHandlerPage.ActionExceptionHandlerPage);
        }

        public static IApplicationBuilder UseCanvasLtiCourseIdentity(this IApplicationBuilder builder, Action<CanvasIdentityOption> configureOptions)
        {
            var options = new CanvasIdentityOption();
            configureOptions(options); // exe action
            return builder.UseMiddleware<CanvasLtiCourseIdentityMiddleware>(options);
        }
    }
}
