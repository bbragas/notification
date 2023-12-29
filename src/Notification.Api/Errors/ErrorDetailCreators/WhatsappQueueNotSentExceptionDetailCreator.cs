using Notification.Api.Infrastructure.Exceptions.Queries;
using System.Net;

namespace Notification.Api.Errors.ErrorDetailCreators
{    
    public class WhatsappQueueNotSentExceptionDetailCreator : IExceptionDetailCreator
    {
        public Type Type => typeof(WhatsappQueueNotSentException);

        public ErrorDetails GetErrorDetails(Exception exception) => new((int)HttpStatusCode.ServiceUnavailable, exception.Message);
    }
}
