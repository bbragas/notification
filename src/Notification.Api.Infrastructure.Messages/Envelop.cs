namespace Notification.Api.Infrastructure.Messages;

public record Envelop(
    Guid Id,
    string SpecVersion,
    string Type,
    string Source,
    string Subject,
    string Time,
    string DataContentType,
    string Data);