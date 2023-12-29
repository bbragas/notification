namespace Notification.Api.Infrastructure.Messages;

public interface INotificationEvent
{
    Guid NotificationId { get; }
    string Description { get; }
    string Type { get; }
    string ToData();
}