namespace Notification.Api.Models.v1.Sms;

public class NotifySmsDTO
{
    public Guid ProjectId { get; init; }
    public Guid ProjectEntityId { get; init; }
    public string Client { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string Campaign { get; init; } = string.Empty;
    public string? SenderName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string? ExternalId { get; init; } = default;
    public DateTime? ScheduledTo { get; init; } = null;
}
