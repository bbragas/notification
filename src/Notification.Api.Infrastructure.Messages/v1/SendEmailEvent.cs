using System.Text.Json.Serialization;
using Notification.Api.Common.Extensions;

namespace Notification.Api.Infrastructure.Messages.v1;

public record struct SendEmailEvent(
    Guid Id,
    string To,
    string Name,
    string Subject,
    string SenderName,
    string Html)
    : INotificationEvent
{
    [JsonIgnore]
    public Guid NotificationId => Id;
    
    [JsonIgnore]
    public string Type => GetType().FullName!;
    
    [JsonIgnore]
    public string Description =>  "The event is triggered when a email notification is requested.";
    
    public string ToData() => this.JsonSerialize();
}

