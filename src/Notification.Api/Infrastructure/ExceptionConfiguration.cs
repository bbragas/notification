using Notification.Api.Errors;
using Notification.Api.Errors.ErrorDetailCreators;

namespace Notification.Api.Infrastructure;
public static class ExceptionConfiguration
{
    public static IServiceCollection AddExceptionServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IExceptionDetailFactory, ExceptionDetailFactory>()
            .AddSingleton<IExceptionDetailCreator, DefaultExceptionDetailCreator>()
            .AddSingleton<IExceptionDetailCreator, ArgumentExceptionDetailCreator>()
            .AddSingleton<IExceptionDetailCreator, BlacklistItemFoundExceptionDetailCreator>()
            .AddSingleton<IExceptionDetailCreator, WhatsappQueueNotSentExceptionDetailCreator>()
            .AddSingleton<IExceptionDetailCreator, ResourceNotFoundExceptionDetailCreator>();

        return services;
    }

    public static void UseExceptionConfigure(this IApplicationBuilder app, IServiceProvider services)
    {
        app.UseExceptionHandler(appError =>
        {
            var exceptionDetailFactory = services.GetService<IExceptionDetailFactory>();
            if (exceptionDetailFactory is not null)
                appError.Use(ExceptionMiddleware(exceptionDetailFactory));
        });
    }

    private static Func<HttpContext, RequestDelegate, Task> ExceptionMiddleware(IExceptionDetailFactory exceptionDetailFactory) =>
        (httpContext, next) => ExceptionMiddlewareWriteResponse.WriteResponse(httpContext, exceptionDetailFactory);
}
