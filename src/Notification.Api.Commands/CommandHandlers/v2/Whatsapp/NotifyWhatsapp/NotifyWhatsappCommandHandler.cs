using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Messages;
using Notification.Api.Infrastructure.Messages.v1.WhatsApp;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.Whatsapp.DTOs;
using WhatsappEntity = Notification.Api.Domain.Whatsapp.Whatsapp;

namespace Notification.Api.Commands.CommandHandlers.v2.Whatsapp.NotifyWhatsapp;

public class NotifyWhatsappCommandHandler : IRequestHandler<NotifyWhatsappCommand, NotifyWhatsappResponseDTO>
{
    private readonly IWriteRepository _writeRepository;
    private readonly IPublishService _publishService;
    private readonly ILogger<NotifyWhatsappCommandHandler> _logger;

    public NotifyWhatsappCommandHandler(IWriteRepository writeRepository, IPublishService publishService, ILogger<NotifyWhatsappCommandHandler> logger)
    {
        _writeRepository = writeRepository;
        _publishService = publishService;
        _logger = logger;
    }

    public async Task<NotifyWhatsappResponseDTO> Handle(NotifyWhatsappCommand request, CancellationToken cancellationToken)
    {
        var notification = new WhatsappEntity
        {
            UnitId = request.UnitId,
            ExternalId = request.ExternalId,
            Project = new(request.ProjectId, request.ProjectEntityId, request.ProjectName),
            MessageType = request.MessageType.GetHashCode(),
            Message = request.Message,
            RecipientNumber = request.RecipientNumber,
            Subtitle = request.Subtitle,
            ScheduledTo = request.ScheduledTo,
            Status = request.ScheduledTo.HasValue ? NotificationStatus.Scheduled : NotificationStatus.Fired
        };

        await _writeRepository.CreateAsync(notification, cancellationToken);

        _logger.LogDebug("Whatsapp notification created with success! Notification: {@notification}", notification);

        if (!notification.IsScheduled)
        {
            var whatsAppEvent = BindWhatsAppEvent(request, notification.Id);

            await _publishService.Publish(whatsAppEvent, cancellationToken);

            _logger.LogDebug("Whatsapp notification event has been sent to the SQS message. Event: {@event}", whatsAppEvent);
        }

        return new NotifyWhatsappResponseDTO(notification.Id);
    }

    private INotificationEvent BindWhatsAppEvent(NotifyWhatsappCommand request, Guid notificationId)
    {
        switch (request.MessageType)
        {
            case MessageType.Text:
                return new SendTextWhatsAppEvent(notificationId, request.RecipientNumber, request.Message);
            case MessageType.Link:
                return new SendLinkWhatsAppEvent(notificationId, request.RecipientNumber, request.Subtitle, request.Message);
            case MessageType.UrlMedia:
                return new SendMediaWhatsAppEvent(notificationId, request.RecipientNumber, request.Subtitle, request.Message);
            case MessageType.Base64Media:
                return new SendMediaBase64WhatsAppEvent(notificationId, request.RecipientNumber, request.Subtitle, request.Message);
            default:
                return default;
        }
    }
}
