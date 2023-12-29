namespace Notification.Api.Models.v1.Email;

public class NotifyEmailBatchDTO
{
    public Guid ProjectId { get; set; }
    public string Body { get; set; } = string.Empty;
    public string Client { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string? ExternalId { get; set; } = default;
    public IEnumerable<NotifyEmailBatchCustomerDTO> Customers { get; set; } = Enumerable.Empty<NotifyEmailBatchCustomerDTO>();
    public DateTime? ScheduledTo { get; init; } = null;
}

public class NotifyEmailBatchCustomerDTO
{
    public Guid ProjectEntityId { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();
}
