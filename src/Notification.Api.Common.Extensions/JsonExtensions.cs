using System.Text.Json;
using System.Text.Json.Serialization;

namespace Notification.Api.Common.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions _defaultOptions = GetDefaultOptions();
    private static JsonSerializerOptions GetDefaultOptions()
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringEnumConverter());
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNameCaseInsensitive = true;

        return options;
    }

    public static T? JsonDeserialize<T>(this string json) =>
           JsonSerializer.Deserialize<T>(json, _defaultOptions);

    public static string JsonSerialize<T>(this T value) => 
        JsonSerializer.Serialize(value, _defaultOptions);

}