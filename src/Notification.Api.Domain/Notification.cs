namespace Notification.Api.Domain;

public abstract class Notification
{
    public NotificationStatus Status { get; set; }
    public DateTime? ScheduledTo { get; set; }
    public bool IsScheduled => Status == NotificationStatus.Scheduled;
}