using Microsoft.Extensions.DependencyInjection;
using Notification.Api.Commands.Services;

namespace Notification.Api.Commands;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
                    .AddTransient<ISqsService, SqsService>()
                    .AddTransient<IPublishService, PublishService>();
    }
}
