using Microsoft.Extensions.Options;
using Notification.Api.Gateway.Http.Models;
using Notification.Api.Gateway.Http.Whatsapp;

namespace Notification.Api.Configurations;

public static class WhatsappServiceProviderConfiguration
{
    public static IServiceCollection AddWhatsappServiceProvider(this IServiceCollection services, IConfiguration configuration)
    {
        var whatsappServiceSettings = configuration.GetRequiredSection(nameof(WhatsappProviderSettings)).Get<WhatsappProviderSettings>();
        services.AddSingleton(Options.Create(whatsappServiceSettings));

        services.AddHttpClient<IWhatsappHttpClient, WhatsappHttpClient>(http => WhatsappServiceProviderHttpClientConfig(http, whatsappServiceSettings));

        return services;
    }

    private static void WhatsappServiceProviderHttpClientConfig(HttpClient httpClient, WhatsappProviderSettings whatsappServiceSettings)
    {
        httpClient.BaseAddress = whatsappServiceSettings.BaseUri;
        httpClient.DefaultRequestHeaders.Add(whatsappServiceSettings.AuthHeaderName, whatsappServiceSettings.ApiKey.ToString());
    }
}
