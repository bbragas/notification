namespace Notification.Api.Commands.Helpers;

public static class DynamicBodyHelper
{
    public static string BuildBody(string body, Dictionary<string, string> attributes)
    {
        foreach (var attribute in attributes)
        {
            body = body.Replace("{{" + attribute.Key + "}}", attribute.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        return body;
    }
}
