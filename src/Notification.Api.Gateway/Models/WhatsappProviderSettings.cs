namespace Notification.Api.Gateway.Http.Models;

public class WhatsappProviderSettings
{
    public Uri BaseUri { get; init; }
    public Guid ApiKey { get; init; }
    public string AuthHeaderName { get; init; }
    public Guid ProductId { get; init; }
    public string PhoneId { get; init; }
}
