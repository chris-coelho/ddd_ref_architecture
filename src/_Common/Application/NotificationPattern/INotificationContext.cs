using FluentValidation.Results;

namespace Common.Application.NotificationPattern;

public interface INotificationContext
{
    IReadOnlyCollection<Notification> Notifications { get; }
    bool HasNotifications { get; }
    NotificationType NotificationType { get; set; }

    void AddAsAppService(string message, string? logMessage = null);
    void AddAsDomainValidation(string message, ValidationResult validationResult, string? logMessage = null);
        
    void AddNotification(string message, string? logMessage = null);
    void AddNotification(string message, Exception exception, string? logMessage = null);
}