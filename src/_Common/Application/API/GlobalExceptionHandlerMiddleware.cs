using System.Net.Mime;
using Common.Application.NotificationPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Application.API;

public class GlobalExceptionHandlerMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware
    (
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvariantViolationException ex)
        {
            var logMessage = $"A database exception occurred. Details: :{ex.Message}";
            const string friendlyMessage = "An internal validation database error occurred. Contact the support.";
            _logger.LogError(ex, logMessage);

            var json = new DefaultErrorResponse(NotificationType.ApplicationRules, friendlyMessage);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
        catch (Exception ex)
        {
            var logMessage = $"An unexpected error occurred. Please try again later. Details: :{ex.Message}";
            const string friendlyMessage = "An unexpected error occurred. Please try again later.";
            _logger.LogError(ex, logMessage);

            var json = new DefaultErrorResponse(NotificationType.Undefined, friendlyMessage);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}