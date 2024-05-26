using System.Diagnostics;
using System.Net;
using CanvasIdentity.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


using Microsoft.Extensions.DependencyInjection;

namespace CanvasIdentity.ExceptionPage
{
    public static class ExceptionHandlerPage
    {
        public static void ActionExceptionHandlerPage(IApplicationBuilder builder)
        {
            builder.Run(async context =>
            {
                 var loggerFactory = context.RequestServices.GetService<ILoggerFactory>();
                 var logger = loggerFactory.CreateLogger("ExceptionHandlerPage");

                context.Response.ContentType = "text/html";
                context.Response.Cookies.Delete("PocTokenKpiA");
                var ex = context.Features.Get<IExceptionHandlerFeature>();
                
                if (ex != null)
                {
                    var errorMessage = ex.Error.Message;
                    var requestId = Activity.Current?.Id ?? context.TraceIdentifier;
                    var statusCode = HttpStatusCode.InternalServerError;
                    
                    if (ex.Error.GetType() == typeof(IdentityHttpException))
                    {
                        if (ex.Error is IdentityHttpException e)
                        {
                            errorMessage = $"CanvasIdentity: {e.Message} StatusCode:{e.StatusCode}";
                            statusCode = e.StatusCode;
                        }
                    }

                     logger.LogError($"{errorMessage} {requestId} {ex.Error.InnerException}");

                    var err = $"<h2>{errorMessage}</h2> " +
                              $"RequestId {requestId}";

                    context.Response.StatusCode = (int)statusCode;
                    await context.Response.WriteAsync(err).ConfigureAwait(false);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("error").ConfigureAwait(false);
                }
            });
        }
    }
}
