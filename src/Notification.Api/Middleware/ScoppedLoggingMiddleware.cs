namespace Notification.Api.Middleware;

public class ScoppedLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ScoppedLoggingMiddleware(
        RequestDelegate next,
        ILogger<ScoppedLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var state = new Dictionary<string, object>
        {
            [MandatoryHeaders.TraceId] = httpContext.Request.Headers[MandatoryHeaders.TraceId].ToString(),
            [MandatoryHeaders.UserAgent] = httpContext.Request.Headers[MandatoryHeaders.UserAgent].ToString()
        };

        using (_logger.BeginScope(state))
        {
            await _next(httpContext);
        }
    }
}
