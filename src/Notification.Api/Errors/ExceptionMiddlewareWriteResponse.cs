using Microsoft.AspNetCore.Diagnostics;

namespace Notification.Api.Errors;

public static class ExceptionMiddlewareWriteResponse
{
    public static Task WriteResponse(HttpContext httpContext, IExceptionDetailFactory exceptionDetailFactory)
    {
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var ex = exceptionDetails?.Error;

        if (ex == null)
            return Task.CompletedTask;

        var errorDetail = exceptionDetailFactory.GetErrorsDetails(ex);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = errorDetail.StatusCode;

        return httpContext.Response.WriteAsync(errorDetail.ToString());
    }
}
