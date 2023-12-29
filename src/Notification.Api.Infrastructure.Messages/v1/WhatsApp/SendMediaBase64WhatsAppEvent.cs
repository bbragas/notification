using System.Text.Json.Serialization;
using Notification.Api.Common.Extensions;

namespace Notification.Api.Infrastructure.Messages.v1.WhatsApp;

public record class SendMediaBase64WhatsAppEvent(
    Guid NotificationId,
    string To,
    string Subtitle,
    string MediaBase64) : INotificationEvent
{
    [JsonIgnore]
    public string Type => GetType().FullName!;
    
    [JsonIgnore]
    public string Description =>  "The event is triggered when a WhatsApp  base64 encoded media notification is requested.";
    
    public string ToData() => this.JsonSerialize();
}
