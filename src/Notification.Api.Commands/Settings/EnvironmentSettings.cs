namespace Notification.Api.Commands.Settings;

public class EnvironmentSettings
{
    public string Type { get; set; } = "local";
    public bool HasSwagger { get; set; }
}
