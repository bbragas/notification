using Notification.Api.Infrastructure.Exceptions;
using System.Net;

namespace Notification.Api.Errors.ErrorDetailCreators;

public class BlacklistItemFoundExceptionDetailCreator : IExceptionDetailCreator
{
    public Type Type => typeof(BlacklistItemFoundException);

    public ErrorDetails GetErrorDetails(Exception exception) => new((int)HttpStatusCode.Forbidden, exception.Message);
}
