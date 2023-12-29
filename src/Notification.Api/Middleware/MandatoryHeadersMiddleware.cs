using System.Net;

namespace Notification.Api.Middleware;

public class MandatoryHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public MandatoryHeadersMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.HasValue && !context.Request.Path.Value.Contains("/api/v1"))
        {
            var errors = new List<ErrorCode>();

            if (!context.Request.Headers.TryGetValue(MandatoryHeaders.UserAgent, out var userAgent))
            {
                errors.Add(ErrorCode.UserAgentHeaderIsMandatory);
            }

            if (!context.Request.Headers.TryGetValue(MandatoryHeaders.TraceId, out var traceId))
            {
                errors.Add(ErrorCode.TraceIdHeaderIsMandatory);
            }

            if (errors.Any())
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new { Errors = errors });
                return;
            }
        }
        await _next(context);
    }
}
