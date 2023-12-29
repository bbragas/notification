using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Filters;
using System.Diagnostics;

namespace Notification.Api.Infrastructure;

public static class LogConfiguration
{
    internal static IHostBuilder ConfigureLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", "notification-api")
                .Enrich.FromLogContext();

            configuration.Filter.ByExcluding(exclusion =>
            {
                var isHealthCheckEndpoint = Matching.WithProperty<string>("RequestPath", path => path.Contains("health", StringComparison.InvariantCultureIgnoreCase));
                var isOkStatusCode = Matching.WithProperty<int>("StatusCode", code => code > 400);
                return isHealthCheckEndpoint(exclusion) && (exclusion.Properties.ContainsKey("StatusCode") ? isOkStatusCode(exclusion) : true );
            });

            if (Debugger.IsAttached)
            {
                configuration.WriteTo.Console(theme: SystemConsoleTheme.Literate);
            }
            else
            {
                configuration.WriteTo.Async(c => c.Console(new JsonFormatter(renderMessage: true)));
            }
        });

        return hostBuilder;
    }
}
