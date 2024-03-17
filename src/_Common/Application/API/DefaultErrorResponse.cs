using Common.Application.NotificationPattern;

namespace Common.Application.API;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class DefaultErrorResponse
{
    public DefaultErrorResponse(
        NotificationType type, 
        string message,
        ICollection<string>? errors = null)
    {
        ErrorMessage = message;
        Type = type.ToString();
        Errors = errors ?? new List<string>();
    }

    public string ErrorMessage { get; }
    public string Type { get; }
    public ICollection<string> Errors { get; }
}
