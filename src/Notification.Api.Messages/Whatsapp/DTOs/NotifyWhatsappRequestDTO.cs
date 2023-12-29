using Notification.Api.Messages.Abstractions.DTOs;

namespace Notification.Api.Messages.Whatsapp.DTOs;

public class NotifyWhatsappRequestDTO : IDataTransferObjectValidator
{
    public Guid UnitId { get; init; }
    public Guid ProjecttId { get; init; }
    public Guid? ProjectEntityId { get; init; } = null;
    public string ProjectName { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public MessageType MessageType { get; init; }
    public string? Subtitle { get; init; } = null;
    public string RecipientNumber { get; init; } = string.Empty;
    public string? ExternalId { get; init; } = null;
    public DateTime? ScheduledTo { get; init; } = null;
}

