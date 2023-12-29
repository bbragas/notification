using MediatR;

namespace Notification.Api.Commands.CommandHandlers.v1.NotifySms;

public record struct NotifySmsCommand(
    string Client,
    string Campaign,
    Guid ProjectId,
    Guid ProjectEntityId,
    string Message,
    string? SenderName,
    string PhoneNumber,
    DateTime? ScheduledTo,
    Guid? ExternalId) : IRequest<Unit>;

