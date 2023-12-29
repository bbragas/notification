namespace Notification.Api.Errors;
public interface IExceptionDetailFactory
{
    ErrorDetails GetErrorsDetails(Exception exception);
}