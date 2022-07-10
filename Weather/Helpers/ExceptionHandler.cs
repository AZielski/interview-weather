using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Weather.Helpers;

public class ErrorResult
{
    public ErrorResult(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    private int StatusCode { get; set; }
    private string Message { get; set; }
}

public static class ExceptionHandler
{
    public static void ConfigureHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("");
                
                logger.LogError(context.Features.Get<IExceptionHandlerPathFeature>()?.Error, "Error occured");
            });
        });
    }
}