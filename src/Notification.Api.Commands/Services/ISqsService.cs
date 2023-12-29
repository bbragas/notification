using Notification.Api.Infrastructure.Messages;

namespace Notification.Api.Commands.Services;

public interface ISqsService
{
    Task SendMessageAsync<TMessage>(TMessage message, string queueUrl, CancellationToken cancellationToken);
}
