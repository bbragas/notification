using System.Net;
using Notification.Api.Infrastructure.Exceptions;

namespace Notification.Api.Errors.ErrorDetailCreators;

public class ResourceNotFoundExceptionDetailCreator : IExceptionDetailCreator
{
    public Type Type => typeof(ResourceNotFoundException);

    public ErrorDetails GetErrorDetails(Exception exception) => new((int)HttpStatusCode.NotFound, exception.Message);
}