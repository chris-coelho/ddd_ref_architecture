namespace Common.Application.NotificationPattern;

public class Notification
{
    public string Message { get; }
    public Exception Exception { get; }

    public Notification(string message)
    {
        Message = message;
        Exception = new Exception();
    }

    public Notification(string message, Exception exception)
    {
        Message = message;
        Exception = exception;
    }
}