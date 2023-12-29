using System.Text.Json.Serialization;
using Notification.Api.Common.Extensions;

namespace Notification.Api.Infrastructure.Messages.v1.WhatsApp;

public record class SendTextWhatsAppEvent(
    Guid NotificationId,
    string To,
    string Text) : INotificationEvent
{
    [JsonIgnore]
    public string Type => GetType().FullName!;
    
    [JsonIgnore]
    public string Description =>  "The event is triggered when a WhatsApp text notification is requested.";
    
    public string ToData() => this.JsonSerialize();
}
