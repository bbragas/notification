using System.Net;

namespace Notification.Api.Errors.ErrorDetailCreators;

public class ArgumentExceptionDetailCreator : IExceptionDetailCreator
{
    public Type Type => typeof(ArgumentException);

    public ErrorDetails GetErrorDetails(Exception exception) => new((int)HttpStatusCode.BadRequest, exception.Message);
}