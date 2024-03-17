using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Common.Application.NotificationPattern;

public class NotificationContext: INotificationContext
{
    private readonly List<Notification> _notifications = new();
    private readonly ILogger<NotificationContext> _logger;

    public NotificationContext(ILogger<NotificationContext> logger)
    {
        _logger = logger  ?? throw new ArgumentNullException(nameof(logger));
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Any();
    public NotificationType NotificationType { get; set; } = NotificationType.Undefined;

    public void AddAsAppService(string message, string? logMessage = null)
    {
        NotificationType = NotificationType.ApplicationRules;
        _notifications.Add(new Notification(message));
        _logger.LogWarning(logMessage ?? message);
    }

    public void AddAsDomainValidation(string message, ValidationResult validationResult, string? logMessage = null)
    {
        NotificationType = NotificationType.BusinessRules;
        _notifications.Add(new Notification(message));
        foreach (var error in validationResult.Errors)
            _notifications.Add(new Notification(error.ErrorMessage));
        
        var mens = logMessage is null
                ? $"{message}: {string.Join(';', validationResult.Errors.Select(x => x.ErrorMessage))}"
                : logMessage;
        _logger.LogWarning(mens);
    }

    public void AddNotification(string message, string? logMessage = null)
    {
        _notifications.Add(new Notification(message));
        _logger.LogWarning(logMessage ?? message);
    }

    public void AddNotification(string message, Exception exception, string? logMessage = null)
    {
        _notifications.Add(new Notification(message, exception));
        _logger.LogWarning(exception, logMessage ?? message);
    }
}