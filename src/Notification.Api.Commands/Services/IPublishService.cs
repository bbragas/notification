using Notification.Api.Infrastructure.Messages;

namespace Notification.Api.Commands.Services
{
    public interface IPublishService
    {
        Task Publish(INotificationEvent notificationEvent, CancellationToken cancellationToken);
    }
}

