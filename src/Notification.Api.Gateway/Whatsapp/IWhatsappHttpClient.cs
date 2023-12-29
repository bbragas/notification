using Notification.Api.Gateway.Http.Models;

namespace Notification.Api.Gateway.Http.Whatsapp
{
    public interface IWhatsappHttpClient
    {
        Task<QueueNotSentCountDto?> GetQueueNotSendCountAsync(CancellationToken cancellationToken);
    }
}
