using Notification.Api.Domain.Base;

namespace Notification.Api.Domain.Whatsapp;

public class Whatsapp : NotificationBase
{
    public string Message { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public int MessageType { get; set; }
    public string RecipientNumber { get; set; } = string.Empty;
}
