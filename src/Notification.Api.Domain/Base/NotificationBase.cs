namespace Notification.Api.Domain.Base;

abstract public class NotificationBase: Notification, IIdentifier
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UnitId { get; init; }
    public string? ExternalId { get; init; }
    public ProjectBaseEntity Project { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
