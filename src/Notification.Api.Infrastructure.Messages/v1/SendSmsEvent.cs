﻿using System.Text.Json.Serialization;
using Notification.Api.Common.Extensions;

namespace Notification.Api.Infrastructure.Messages.v1;

public record struct SendSmsEvent(
        Guid Id,
        string Campaign,
        string? SenderName,
        string To,
        string Text)
    : INotificationEvent
{
    [JsonIgnore]
    public Guid NotificationId => Id;
    
    [JsonIgnore]
    public string Type => GetType().FullName!;
    
    [JsonIgnore]
    public string Description =>  "The event is triggered when a SMS notification is requested.";
    
    public string ToData() => this.JsonSerialize();
}

