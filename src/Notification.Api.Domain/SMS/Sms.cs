namespace Notification.Api.Domain;

public class Sms : Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Client { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectEntityId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? SenderName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? ExternalId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
