using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Weather.Helpers;

public static class ExceptionHandler
{
    public static void ConfigureHandler(this IApplicationBuilder app, ILogger logger, bool logToResponse)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                
                await context.Response.WriteAsync((logToResponse ? error?.ToString() : "") ?? string.Empty);
                
                logger.LogError(error, "Error occured");
            });
        });
    }
}