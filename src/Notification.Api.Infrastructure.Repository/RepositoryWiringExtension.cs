using Microsoft.Extensions.DependencyInjection;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers;

namespace Notification.Api.Infrastructure.Repository;

public static class RepositoryWiringExtension
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services
            .AddCollectionMapper()
            .AddSingleton<IDbContext, DbContext>()
            .AddSingleton<IWriteRepository, WriteRepository>()
            .AddSingleton<IReadRepository, ReadRepository>();
    }

    private static IServiceCollection AddCollectionMapper(this IServiceCollection services)
    {
        var type = typeof(ICollectionMapper);
        var types = type.Assembly.GetTypes();

        foreach (var item in types)
        {
            if (!item.IsInterface && typeof(ICollectionMapper).IsAssignableFrom(item))
            {
                services.AddSingleton(type, item);
            }
        }
        return services;
    }
}
