namespace Notification.Api.Middleware;

public class SwaggerHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SwaggerHeadersMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.HasValue &&
            !context.Request.Path.Value.Contains("/api/v1") &&
            !context.Request.Headers.ContainsKey(MandatoryHeaders.TraceId))
        {
            context.Request.Headers.Add(MandatoryHeaders.TraceId, $"Notification.Api.swagger-{Guid.NewGuid()}");
        }

        await _next(context);
    }
}
