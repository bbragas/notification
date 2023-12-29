using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Notification.Api.Infrastructure.Repository.Mongo.Configuration;
using System.Net.Mime;
using System.Text.Json;

namespace Notification.Api.Infrastructure;
public static class HeathCheckConfiguration
{
    internal static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        MongoSettings mongoSettings = configuration.GetSection("Mongo").Get<MongoSettings>();
        services.AddHealthChecks()
           .AddMongoDb(mongoSettings.ConnectionString,
                       failureStatus: HealthStatus.Unhealthy,
                       tags: new[] { "DB" });

        return services;
    }

    internal static IEndpointRouteBuilder UseHealthCheck(this IEndpointRouteBuilder applicationBuilder)
    {
        applicationBuilder.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var result = JsonSerializer.Serialize(
                    new
                    {
                        status = report.Status.ToString(),
                        monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                    });
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
        });

        return applicationBuilder;
    }
}

