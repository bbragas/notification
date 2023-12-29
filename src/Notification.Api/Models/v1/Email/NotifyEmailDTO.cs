namespace Notification.Api.Models.v1.Email;

public class NotifyEmailDTO
{
    public Guid ProjectId { get; init; }
    public Guid ProjectEntityId { get; init; }
    public string Body { get; init; } = string.Empty;
    public string Client { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Campaign { get; init; } = string.Empty;
    public string SenderName { get; init; } = string.Empty;
    public string SenderEmail { get; init; } = string.Empty;
    public string RecipientName { get; init; } = string.Empty;
    public string RecipientEmail { get; init; } = string.Empty;
    public string? ExternalId { get; init; } = default;
    public DateTime? ScheduledTo { get; init; } = null;
}