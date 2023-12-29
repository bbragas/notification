using Microsoft.Extensions.Options;
using Notification.Api.Common.Extensions;
using Notification.Api.Gateway.Http.Models;

namespace Notification.Api.Gateway.Http.Whatsapp
{
    public class WhatsappHttpClient: IWhatsappHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly WhatsappProviderSettings _whatsappServiceSettings;

        public WhatsappHttpClient(HttpClient httpClient, IOptions<WhatsappProviderSettings> whatsappServiceSettings)
        {
            _httpClient = httpClient;
            _whatsappServiceSettings = whatsappServiceSettings.Value;
        }

        public async Task<QueueNotSentCountDto?> GetQueueNotSendCountAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{_whatsappServiceSettings.ProductId}/{_whatsappServiceSettings.PhoneId}/queue", cancellationToken);
            response.EnsureSuccessStatusCode();

            var whatsappNotSentCountContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return whatsappNotSentCountContent.JsonDeserialize<QueueNotSentCountDto>();        
        }
    }
}
