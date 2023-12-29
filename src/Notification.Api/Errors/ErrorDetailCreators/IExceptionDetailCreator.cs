namespace Notification.Api.Errors.ErrorDetailCreators;
public interface IExceptionDetailCreator
{
    public Type Type { get; }
    ErrorDetails GetErrorDetails(Exception exception);
}