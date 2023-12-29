using MediatR;

namespace Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;

public record struct NotifyEmailCommand(
    string Client,
    string Campaign,
    Guid ProjectId,
    Guid ProjectEntityId,
    string SenderName,
    string SenderEmail,
    string RecipientName,
    string RecipientEmail,
    string Subject,
    string Body,
    Guid? ExternalId,
    DateTime? ScheduledTo) : IRequest<Unit>;
