using System.Net;
using System.Text;
using Common.Application.NotificationPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Application.API;

public class NotificationAsyncResultFilter : IAsyncResultFilter
{
    private readonly ILogger<NotificationAsyncResultFilter> _logger;
    private readonly INotificationContext _notification;

    public NotificationAsyncResultFilter(
        ILogger<NotificationAsyncResultFilter> logger,
        INotificationContext notification)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        context.HttpContext.Response.ContentType = "application/json";
        var result = context.Result as ObjectResult;

        if (_notification.HasNotifications) // Business known errors
        {
            context.HttpContext.Response.StatusCode = GetStatusCodeForNotifications();
            _logger.LogDebug(GetLogMessageForNotifications());

            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(GetNotificationErrorResponse()));
            return;
        }

        if (result?.Value != null && result.StatusCode == 400) // BadRequest errors
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var badRequestResult = (ValidationProblemDetails)result.Value;

            _logger.LogWarning(GetLogMessageForBadRequest(badRequestResult));

            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(GetBadRequestErrorResponse(badRequestResult)));
            return;
        }

        if (result?.Value != null 
            && result.StatusCode >= 200 && result.StatusCode < 300)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            
            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result.Value));
            return;
        }

        await next();
    }

    private int GetStatusCodeForNotifications()
    {
        return _notification.NotificationType switch
        {
            NotificationType.BusinessRules => (int) HttpStatusCode.BadRequest,
            NotificationType.ApplicationRules => (int) HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError
        };
    }

    private string GetLogMessageForNotifications()
    {
        var logMessage = new StringBuilder();
        foreach (var notification in _notification.Notifications)
        {
            logMessage.Append(notification.Message);
            logMessage.Append($"Details: {notification.Exception}");
        }

        return logMessage.ToString();
    }

    private string GetLogMessageForBadRequest(ValidationProblemDetails problemDetails)
    {
        var logMessage = new StringBuilder();
        logMessage.Append($"A BadRequest occurred: {problemDetails.Title}, Errors:");
        foreach (var error in problemDetails.Errors)
        {
            for (var i = 0; i < error.Value.Length; i++)
            {
                logMessage.Append($"Key: {error.Key}, Value: {error.Value[i]}");
            }
        }

        return logMessage.ToString();
    }

    private DefaultErrorResponse GetNotificationErrorResponse()
    {
        var message = "Request failure";
        if (_notification.NotificationType == NotificationType.BusinessRules)
            message = _notification.Notifications.FirstOrDefault()!.Message;

        var errors = _notification.Notifications.Where(x => x.Message != message)
            .Select(x => x.Message).ToList();

        return new DefaultErrorResponse(_notification.NotificationType, message, errors);
    }

    private DefaultErrorResponse GetBadRequestErrorResponse(ValidationProblemDetails problemDetails)
    {
        var errors = new List<string>();
        foreach (var error in problemDetails.Errors)
            errors.AddRange(error.Value.Select(t => $"Key: {error.Key}, Value: {t}"));

        return new DefaultErrorResponse(_notification.NotificationType, "Request failure", errors);
    }
}