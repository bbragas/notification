using Notification.Api.Commands.Settings;
using Notification.Api.Infrastructure.Repository.Mongo.Configuration;

namespace Notification.Api.Infrastructure;
public static class SettingsConfiguration
{

    internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("Mongo"));
        services.Configure<NotifySettings>(configuration.GetSection("NotifyEvent"));

        return services;
    }
}

