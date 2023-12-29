namespace Notification.Api.Domain;

public class Email : Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Client { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public Guid ProjectEntityId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public Guid? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
