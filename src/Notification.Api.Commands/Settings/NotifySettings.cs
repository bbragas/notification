namespace Notification.Api.Commands.Settings;

public class NotifySettings
{
    public string QueueUrl { get; set; } = string.Empty;
    public string SpecVersionEvent { get; set; } = string.Empty;
    public string SourceEvent { get; set; } = string.Empty;
    public string DataContentTypeEvent { get; set; } = string.Empty;
};
