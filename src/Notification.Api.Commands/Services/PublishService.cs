using System.Globalization;
using Microsoft.Extensions.Options;
using Notification.Api.Commands.Settings;
using Notification.Api.Infrastructure.Messages;

namespace Notification.Api.Commands.Services;

public class PublishService : IPublishService
{
    private readonly NotifySettings _notifySettings;
    private readonly ISqsService _sqsService;

    public PublishService(IOptions<NotifySettings> notifySmsSettings, ISqsService sqsService)
    {
        _notifySettings = notifySmsSettings.Value;
        _sqsService = sqsService;
    }

    public Task Publish(INotificationEvent notificationEvent, CancellationToken cancellationToken)
    {
        var envelop = new Envelop(
            notificationEvent.NotificationId,
            _notifySettings.SpecVersionEvent,
            notificationEvent.Type,
            _notifySettings.SourceEvent,
            notificationEvent.Description,
            DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
            _notifySettings.DataContentTypeEvent,
            notificationEvent.ToData()
        );

        return _sqsService.SendMessageAsync(envelop, _notifySettings.QueueUrl, cancellationToken);
    }
}